using System;
using System.Collections;
using System.Collections.Generic;
using TopDevLinks.Infrastructure;

namespace TopDevLinks.Tests.Controllers
{
    public abstract class DisconnectedControllerTestFixture<TController> where TController : MongoContextController
    {
        private TController _controller;
        private Func<dynamic, dynamic> _executeQueryFunc;
        private List<object> _receivedQueries;
        private Queue _queryResponses;

        public DisconnectedControllerTestFixture()
        {
            _receivedQueries = new List<object>();
            _queryResponses = new Queue();
            _executeQueryFunc = query =>
                                    {
                                        _receivedQueries.Add(query);
                                        return _queryResponses.Dequeue();
                                    };
        }

        protected abstract TController CreateController();

        protected void AddQueryResponse(object response)
        {
            _queryResponses.Enqueue(response);
        }

        protected TQuery GetReceivedQuery<TQuery>(int index)
        {
            return (TQuery)_receivedQueries[index];
        }

        protected TController Controller
        {
            get
            {
                if (_controller == null)
                {
                    _controller = CreateController();
                    _controller.ExecuteQueryFunc = _executeQueryFunc;
                }

                return _controller;
            }
        }
    }
}