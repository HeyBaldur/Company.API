// <copyright file="IGenericReturnableHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Company.API.Helpers
{
    using System.Net;
    using Microsoft.AspNetCore.Mvc;

    public interface IGenericReturnableHelper
    {
        IActionResult GenericReturnableObject<T>(HttpStatusCode httpStatusCode, T data);
    }
}
