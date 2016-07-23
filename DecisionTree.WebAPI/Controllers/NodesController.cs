using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;
using DecisionTree.Model;
using DecisionTree.Repository;
using Microsoft.OData.Core;

namespace DecisionTree.WebAPI.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using DecisionTree.Model;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Node>("Nodes");
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class NodesController : ODataController
    {
        private static ODataValidationSettings _validationSettings = new ODataValidationSettings();
        private readonly DecisionTreeContext context;

        public NodesController()
        {
            context = new DecisionTreeContext();
        }

        [ODataRoute]
        [HttpGet]
        [EnableQuery]
        public IHttpActionResult GetNodes()
        {
            return Ok<IEnumerable<Node>>(context.Nodes);
        }

        // GET: odata/Nodes(5)
        public IHttpActionResult GetNode([FromODataUri] System.Guid key, ODataQueryOptions<Node> queryOptions)
        {
            // validate the query.
            try
            {
                queryOptions.Validate(_validationSettings);
            }
            catch (ODataException ex)
            {
                return BadRequest(ex.Message);
            }

            // return Ok<Node>(node);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PUT: odata/Nodes(5)
        public IHttpActionResult Put([FromODataUri] System.Guid key, Delta<Node> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Put(node);

            // TODO: Save the patched entity.

            // return Updated(node);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // POST: odata/Nodes
        public IHttpActionResult Post(Node node)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Add create logic here.

            // return Created(node);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // PATCH: odata/Nodes(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] System.Guid key, Delta<Node> delta)
        {
            Validate(delta.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // TODO: Get the entity here.

            // delta.Patch(node);

            // TODO: Save the patched entity.

            // return Updated(node);
            return StatusCode(HttpStatusCode.NotImplemented);
        }

        // DELETE: odata/Nodes(5)
        public IHttpActionResult Delete([FromODataUri] System.Guid key)
        {
            // TODO: Add delete logic here.

            // return StatusCode(HttpStatusCode.NoContent);
            return StatusCode(HttpStatusCode.NotImplemented);
        }
    }
}
