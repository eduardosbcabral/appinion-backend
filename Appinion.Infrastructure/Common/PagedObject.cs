using NHibernate;
using NHibernate.Transform;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appinion.Infrastructure.Common
{
    /// <summary>
    /// Classe que faz a implementação da paginação nas consultas.
    /// </summary>
    /// <typeparam name="T">Recebe qualquer tipo de objeto.</typeparam>
    public class PagedObject<T>
    {
        private double rowCount;
        private double pageCount;
        private int pageSize;
        public IQueryOver<T> ResultQuery { get; protected set; }

        public PagedObject<T> Paginate(IQueryOver<T> query, int pageSize, int pageNumber)
        {
            this.rowCount = query.RowCount();
            this.pageCount = Math.Ceiling(this.rowCount / pageSize);
            this.pageSize = pageSize;
            this.ResultQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return this;
        }
        public PagedObject<T> Paginate(IQueryOver<T, T> query, int pageSize, int pageNumber, IResultTransformer transformer)
        {
            query = query.TransformUsing(transformer);
            this.rowCount = query.RowCount();
            this.pageCount = Math.Ceiling(this.rowCount / pageSize);
            this.pageSize = pageSize;
            this.ResultQuery = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return this;
        }

        public object PageResult(object dataRows)
        {
            return new
            {
                Rows = dataRows,
                RowCount = this.rowCount,
                PageCount = this.pageCount,
                PageSize = this.pageSize
            };
        }

    }
}
