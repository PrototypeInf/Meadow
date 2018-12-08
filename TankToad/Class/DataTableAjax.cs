using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using LinqKit;
using System.Linq.Expressions;
using System.Linq.Dynamic;
using System.Data.Entity;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Diagnostics;

using TankToad.Controllers;

namespace DataTableAjax
{
    #region Models
    public class DataTableAjaxPostModel
    {
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
    #endregion

    public class DataTableQuery
    {
        private DataTableAjaxPostModel _model;
        private string _searchBy, _sortBy;
        private int _take, _skip, _filteredResultsCount, _totalResultsCount;
        private bool _sortDir;

        public DataTableAjaxPostModel Model { get { return _model; } }
        public int FilteredResultsCount { get { return _filteredResultsCount; } }
        public int TotalResultsCount { get { return _totalResultsCount; } }

        private object GetProperty<T>(object obj, string propName)
        {
            if (propName.Contains("."))
            {
                var left = (new Regex(@"^(\w+)\."));
                var right = (new Regex(@"\.(\w+)$"));
                var world = (new Regex(@"(\w+)"));
                var objName = left.Match(propName).Value;
                objName = world.Match(objName).Value;
                var propN = right.Match(propName).Value;
                propN = world.Match(propN).Value;

                var objClass = obj.GetType().GetProperty(objName).GetValue(obj, null);
                if (objClass == null)
                    return null;
                else
                {
                    if (objClass.GetType().GetProperty(propN) == null)
                        return null;
                    return objClass.GetType().GetProperty(propN).GetValue(objClass, null);
                }
            }
            else
            {
                if (obj.GetType().GetProperty(propName) == null)
                    return null;
                return obj.GetType().GetProperty(propName).GetValue(obj, null);
            }
        }
        private Expression<Func<T, bool>> BuildDynamicWhereClause<T>()
        {
            var predicateMain = PredicateBuilder.New<T>(true);

            if (String.IsNullOrWhiteSpace(_searchBy) == false)
            {
                var searchTerms = _searchBy.Split(' ').ToList().ConvertAll(x => x.ToLower());
                foreach (var s in searchTerms)
                {
                    var predicate = PredicateBuilder.New<T>(true);
                    if (s != "")
                        foreach (var p in _model.columns)
                        {
                            string propName = p.data;
                            if (propName != null)
                                predicate = predicate.Or(
                                    d => ("d.Id").ToString().ToLower().Contains(s)); //GetProperty<T>(d, propName) == null ? false : GetProperty<T>(d, propName).ToString().ToLower().Contains(s));
                        }
                    predicateMain.And(predicate);
                }
            }
            return predicateMain;
        }

        public List<T> GetData<T>(DataTableAjaxPostModel model, IQueryable<T> dbSet, bool fullCount=true, Type type = null)
        {        
            _model = model;
            foreach (var c in _model.columns)
            {
                c.data = c.data != null? c.data.Replace("Extends.", ""):null;
            }

            _searchBy = (_model.search != null) ? _model.search.value : null;
            _take = _model.length;
            _skip = _model.start;
            _sortBy = "";
            _sortDir = true;
            if (_model.order != null)
            {
                _sortBy = _model.columns[_model.order[0].column].data;
                _sortDir = _model.order[0].dir.ToLower() == "asc";
            }
            
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), typeof(T).GetProperty(_sortBy).PropertyType);

            var parameter = Expression.Parameter(typeof(T), "t");
            var variable = Expression.Property(parameter, typeof(T).GetProperty(_sortBy));
            //var comparison = Expression.GreaterThan(Expression.Property(parameter, Type.GetType("ConsoleApp6.Album").GetProperty("Quantity")), Expression.Constant(100));
try
            {
            var discountFilterExpression = Expression.Lambda<Func<T,object>> (variable,parameter);

            //Expression<Func<T>>expression=discountFilterExpression

            //var sortV =type.GetProperty(_sortBy);

            

                var whereClause = BuildDynamicWhereClause<T>();
                var result = dbSet
                      .Where(whereClause);
                if (_sortDir)
                    result = result.OrderBy(discountFilterExpression);
                else
                    result = result.OrderByDescending(discountFilterExpression);

                var res = result.Skip(_skip).Take(_take).ToList();

                _filteredResultsCount = dbSet.Where(whereClause).Count();

                if (fullCount)
                    _totalResultsCount = dbSet.Count();
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        

    }
}