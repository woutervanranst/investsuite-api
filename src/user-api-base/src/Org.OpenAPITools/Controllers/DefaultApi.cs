/*
 * user-api
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerGen;
using Newtonsoft.Json;
using Org.OpenAPITools.Attributes;
using Org.OpenAPITools.Models;

namespace Org.OpenAPITools.Controllers
{ 
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    public class DefaultApiController : ControllerBase
    { 
        /// <summary>
        /// Get User Info by User ID
        /// </summary>
        /// <remarks>Retrieve the information of the user with the matching user ID.</remarks>
        /// <param name="userId">Id of an existing user.</param>
        /// <response code="200">User Found</response>
        /// <response code="404">User Not Found</response>
        [HttpGet]
        [Route("/users/{userId}")]
        [ValidateModelState]
        [SwaggerOperation("GetUsersUserId")]
        [SwaggerResponse(statusCode: 200, type: typeof(User), description: "User Found")]
        public virtual IActionResult GetUsersUserId([FromRoute (Name = "userId")][Required]Object userId)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(User));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            string exampleJson = null;
            exampleJson = "{\n  \"firstName\" : \"\",\n  \"lastName\" : \"\",\n  \"emailVerified\" : \"\",\n  \"dateOfBirth\" : \"1997-10-31\",\n  \"id\" : \"\",\n  \"email\" : \"\",\n  \"createDate\" : \"\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<User>(exampleJson)
            : default(User);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Your GET endpoint
        /// </summary>
        /// <param name="userId"></param>
        /// <response code="204">No Content</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [Route("/users/{userId}/exists")]
        [ValidateModelState]
        [SwaggerOperation("GetUsersUserIdExists")]
        public virtual IActionResult GetUsersUserIdExists([FromRoute (Name = "userId")][Required]Object userId)
        {

            //TODO: Uncomment the next line to return response 204 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(204);
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);

            throw new NotImplementedException();
        }

        /// <summary>
        /// Update User Information
        /// </summary>
        /// <remarks>Update the information of an existing user.</remarks>
        /// <param name="userId">Id of an existing user.</param>
        /// <param name="patchUsersUserIdRequest">Patch user properties to update.</param>
        /// <response code="200">User Updated</response>
        /// <response code="404">User Not Found</response>
        /// <response code="409">Email Already Taken</response>
        [HttpPatch]
        [Route("/users/{userId}")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("PatchUsersUserId")]
        [SwaggerResponse(statusCode: 200, type: typeof(User), description: "User Updated")]
        public virtual IActionResult PatchUsersUserId([FromRoute (Name = "userId")][Required]Object userId, [FromBody]PatchUsersUserIdRequest patchUsersUserIdRequest)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(User));
            //TODO: Uncomment the next line to return response 404 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(404);
            //TODO: Uncomment the next line to return response 409 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(409);
            string exampleJson = null;
            exampleJson = "{\n  \"firstName\" : \"\",\n  \"lastName\" : \"\",\n  \"emailVerified\" : \"\",\n  \"dateOfBirth\" : \"1997-10-31\",\n  \"id\" : \"\",\n  \"email\" : \"\",\n  \"createDate\" : \"\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<User>(exampleJson)
            : default(User);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }

        /// <summary>
        /// Create New User
        /// </summary>
        /// <remarks>Create a new user.</remarks>
        /// <param name="postUserRequest">Post the necessary fields for the API to create a new user.</param>
        /// <response code="200">User Created</response>
        /// <response code="400">Missing Required Information</response>
        /// <response code="409">Email Already Taken</response>
        [HttpPost]
        [Route("/user")]
        [Consumes("application/json")]
        [ValidateModelState]
        [SwaggerOperation("PostUser")]
        [SwaggerResponse(statusCode: 200, type: typeof(User), description: "User Created")]
        public virtual IActionResult PostUser([FromBody]PostUserRequest postUserRequest)
        {

            //TODO: Uncomment the next line to return response 200 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(200, default(User));
            //TODO: Uncomment the next line to return response 400 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(400);
            //TODO: Uncomment the next line to return response 409 or use other options such as return this.NotFound(), return this.BadRequest(..), ...
            // return StatusCode(409);
            string exampleJson = null;
            exampleJson = "{\n  \"firstName\" : \"\",\n  \"lastName\" : \"\",\n  \"emailVerified\" : \"\",\n  \"dateOfBirth\" : \"1997-10-31\",\n  \"id\" : \"\",\n  \"email\" : \"\",\n  \"createDate\" : \"\"\n}";
            
            var example = exampleJson != null
            ? JsonConvert.DeserializeObject<User>(exampleJson)
            : default(User);
            //TODO: Change the data returned
            return new ObjectResult(example);
        }
    }
}
