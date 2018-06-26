using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Fina_Poll_System
{
    public interface IEntity
    {
        int ID { get; set; }
    }


    public static class DbContextExtension
    {
        public static void SaveOld<TEntity>(this Fina_Poll_SystemEntities context, TEntity entity) where TEntity : class, IEntity
        {
            var set = context.Set<TEntity>();

            var dbentity = set.Find(entity.ID);

            if (dbentity != null)
            {
                var entry = context.Entry<TEntity>(dbentity);

                entry.CurrentValues.SetValues(entity);
            }
            else
            {
                set.Add(entity);
            }

            context.SaveChanges();
        }



        public static void Save<TEntity>(this Fina_Poll_SystemEntities context, TEntity entity) where TEntity : class, IEntity
        {
            var set = context.Set<TEntity>();
            //ppc 

            if (entity.ID > 0)
            {
                //update

                if (!context.Set<TEntity>().Local.Any(t => t == entity || t.ID == entity.ID))
                {

                    set.Attach(entity);

                    context.Entry(entity).State = EntityState.Modified;
                }
                else
                {
                    var existingEntity = context.Set<TEntity>().Local.Single(t => t == entity || t.ID == entity.ID);

                    context.Entry<TEntity>(existingEntity).CurrentValues.SetValues(entity);

                    context.Entry<TEntity>(existingEntity).State = EntityState.Modified;
                }

            }
            else
            {
                //add
                set.Add(entity);
            }

            context.SaveChanges();

            //entity.ID = dbentity.ID;

        }

        public static void Delete<TEntity>(this Fina_Poll_SystemEntities context, TEntity entity) where TEntity : class, IEntity
        {
            var set = context.Set<TEntity>();

            var dbentity = set.Find(entity.ID);

            if (dbentity != null)
            {
                set.Remove(dbentity);

                context.SaveChanges();
            }
        }
    }

    public delegate void mydelegate();

    public class BatchSave
    {
        public Fina_Poll_SystemEntities contxt;

        public Dictionary<int, List<BatchItem>> Items { get; set; }
        public BatchItem SaveQuery(BatchItem item, int order)
        {
            if (!Items.ContainsKey(order))
                Items[order] = new List<BatchItem>();
            Items[order].Add(item);

            return item;
        }
    }

    public class BatchItem
    {
        public IEntity entity;
        public mydelegate preprocess;
        public mydelegate postprocess;

        public void Preprocess()
        {
            if (preprocess != null)
                preprocess();
        }

        public void PostProcess()
        {
            if (postprocess != null)
                postprocess();
        }
    }
}