using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Api.Common
{
    public class ApiControllerBase : ControllerBase
    {
        protected ObjectResult Created<T>(T id)
        {
            return StatusCode(StatusCodes.Status201Created, new CreatedResponse<T> { Id = id });
        }

        protected ObjectResult Created<T>(T[] ids)
        {
            return StatusCode(StatusCodes.Status201Created, ids.Select(id => new CreatedResponse<T> { Id = id}));
        }
    }
}
