using Fina_Poll_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fina_Poll_System
{
    public class DataMapper
    {
        public PollModel MapData(Polls entity)
        {
            return new PollModel
            {
                ID = entity.ID,
                TDate = entity.TDate,
                WaitDays = entity.WaitDays,
                Version = entity.Version,
                PollItems = entity.PollItems.Select(MapData).OrderBy(pi=>pi.OrderIndex).ToList()
            };
        }

        public Polls MapData(PollModel model)
        {
            return new Polls
            {
                ID = model.ID,
                TDate = model.TDate,
                WaitDays = model.WaitDays,
                Version = model.Version
            };
        }



        public PollItemModel MapData(PollItems entity)
        {
            return new PollItemModel
            {
                ID = entity.ID,
                PollID = entity.PollID,
                Name = entity.Name,
                PollItemTypes = entity.PollItemTypes.Select(MapData).OrderBy(pit => pit.OrderIndex).ToList(),
                OrderIndex = entity.OrderIndex,
                Description = entity.Description
            };
        }

        public PollItems MapData(PollItemModel model)
        {
            return new PollItems
            {
                ID = model.ID,
                PollID = model.PollID,
                Name = model.Name,
                OrderIndex = model.OrderIndex,
                Description = model.Description
            };
        }



        public PollItemTypeModel MapData(PollItemTypes entity)
        {
            return new PollItemTypeModel
            {
                ID = entity.ID,
                PollItemID = entity.PollItemID,
                Name = entity.Name,
                OrderIndex = entity.OrderIndex
            };
        }

        public PollItemTypes MapData(PollItemTypeModel model)
        {
            return new PollItemTypes
            {
                ID = model.ID,
                PollItemID = model.PollItemID,
                Name = model.Name,
                OrderIndex = model.OrderIndex
            };
        }
    }
}