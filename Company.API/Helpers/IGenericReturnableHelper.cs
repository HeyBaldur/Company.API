// <copyright file="IGenericReturnableHelper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Company.API.Helpers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Net;

    public interface IGenericReturnableHelper
    {
        IActionResult GenericReturnableObject<T>(HttpStatusCode httpStatusCode, T data);
    }
}
