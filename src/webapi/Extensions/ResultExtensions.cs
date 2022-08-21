using System.Threading.Tasks;
using CoffeeRoastery.BLL.Interface.Common;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Extensions;

public static class ResultExtensions
{
    public static ActionResult ToActionResult<T>(this Result<T> result)
    {
        if (!result.IsSucceed)
        {
            return new BadRequestObjectResult(result.ErrorMessage);
        }

        return new OkObjectResult(result.Value);
    }

    public static async Task<IActionResult> ToActionResult(this Task<Result> resultTask)
    {
        var result = await resultTask;

        if (!result.IsSucceed)
        {
            return new BadRequestObjectResult(result.ErrorMessage);
        }

        return new OkResult();
    }

    public static async Task<IActionResult> ToActionResult<T>(this Task<Result<T>> resultTask)
    {
        var result = await resultTask;

        return result.ToActionResult();
    }   
}