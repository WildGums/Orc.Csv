// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityPluralService.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    public class EntityPluralService : IEntityPluralService
    {
        public string ToPlural(string entity)
        {
            if (!entity.EndsWith("s"))
            {
                entity += "s";
            }

            return entity;
        }

        public string ToSingular(string entity)
        {
            if (entity.EndsWith("s"))
            {
                entity = entity.Substring(0, entity.Length - 1);
            }

            return entity;
        }
    }
}