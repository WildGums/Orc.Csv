// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IEntityPluralService.cs" company="WildGums">
//   Copyright (c) 2008 - 2016 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orc.Csv
{
    public interface IEntityPluralService
    {
        string ToPlural(string entity);
        string ToSingular(string entity);
    }
}