using Fina_Poll_System.Filters;
using Fina_Poll_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fina_Poll_System
{
    public class BusinessLogic
    {
        Fina_Poll_SystemEntities _finaEntities;
        public Fina_Poll_SystemEntities FINAEntities
        {
            set
            {
                _finaEntities = value;
            }
            get
            {
                return _finaEntities ?? (_finaEntities = new Fina_Poll_SystemEntities());
            }
        }

        Fina_Poll_SystemEntities context = new Fina_Poll_SystemEntities();
        DataMapper dm = new DataMapper();

        public List<PollModel> GetPolls(BaseFilterModel filter)
        {
            var result = context.Polls.AsQueryable();

            result = result.OrderByDescending(r => r.ID);

            filter.count = result.Count();
            if (filter.start != null)
            {
                result = result.Skip(filter.start.Value);
            }
            if (filter.limit != null)
            {
                result = result.Take(filter.limit.Value);
            }

            return result.Select(dm.MapData).ToList();
        }



        public PollModel SavePollModel(PollModel model)
        {
            var result = dm.MapData(model);

            if (model.Deleted)
            {
                if (model.PollItems != null)
                {
                    foreach (var pollItem in model.PollItems)
                    {
                        pollItem.Deleted = true;
                        SavePollItemModel(pollItem);
                    }
                }

                FINAEntities.Delete(result);
            }
            else
            {
                FINAEntities.Save(result);
                model.ID = result.ID;

                if (model.PollItems != null)
                {
                    foreach (var pollItem in model.PollItems)
                    {
                        if (pollItem.Deleted)
                        {
                            if (pollItem.PollItemTypes != null)
                            {
                                foreach (var pollItemType in pollItem.PollItemTypes)
                                {
                                    FINAEntities.Delete(dm.MapData(pollItemType));
                                }
                            }
                            FINAEntities.Delete(dm.MapData(pollItem));
                        }
                        else
                        {
                            pollItem.PollID = result.ID;
                            SavePollItemModel(pollItem);
                        }
                    }
                }
            }

            return model;
        }



        public void SavePollItemModel(PollItemModel model)
        {
            var result = dm.MapData(model);

            var pollItemTypes = model.PollItemTypes;

            if (model.Deleted)
            {
                if (pollItemTypes != null)
                {
                    pollItemTypes.ToList().ForEach(pit =>
                    {
                        FINAEntities.Delete(dm.MapData(pit));
                    });
                }
                FINAEntities.Delete(result);
            }
            else
            {
                FINAEntities.Save(result);
                if (pollItemTypes != null)
                {
                    pollItemTypes.ToList().ForEach(pit =>
                    {
                        if (pit.Deleted)
                        {
                            FINAEntities.Delete(dm.MapData(pit));
                        }
                        else
                        {
                            pit.PollItemID = result.ID;
                            FINAEntities.Save(dm.MapData(pit));
                        }
                    });
                }
            }
        }
    }
}