using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BoldchatEncryption;
using Microsoft.Web.WebSockets;

namespace WebAPIForBoldchat.Controllers
{
    [EnableCors("*", "*", "GET")]
    public class UtilController : ApiController
    {
        private readonly Sha512Util _shaUtil;
        private readonly PgpUtil _pgpUtil;

        public UtilController()
        {
            //const string hashKey = "+N2HlELtLtpx6N2kWNNLb8fsIdj0dige9xEGB34RvybUzxRkQhkTmI30kSYVN4Mc";
            //const string hashKey = "KZP92SRWYnqO7vUAlQZ7WXOT8+JQyL5P4t4zolqttK2gBfEty/tJvIaFuoOqRF4h";
            //const string hashKey = "rL7IT0hFxPznG1LVno6ljBzGZnwnWj81TcQpxqEqf74iYUKwo39IRsi6/n0NwlZ7";
            const string hashKey = "ql8mUeeCDFfz/qzCQd/RGNML2FmeMBWNoE9QJ+dfbJKm1BFE3AZ6pGkqk833Bubx";
            _shaUtil = new Sha512Util(hashKey);
            _pgpUtil = new PgpUtil();
        }

        // GET: Util
        public IEnumerable<string> Get()
        {
            return new[] { "value3", "value4" };
        }

        [Route("api/util/ws/")]
        public HttpResponseMessage GetWsConnection()
        {
            HttpContext.Current.AcceptWebSocketRequest(new ChatWebSocketHandler(this));
            return Request.CreateResponse(HttpStatusCode.SwitchingProtocols);
        }

        [Route("api/util/hash/{hashKey}/sha512/{input}")]
        public HttpResponseMessage GetSha512EncryptedUsingHash(string hashKey, string input)
        {
            if (string.IsNullOrEmpty(hashKey))
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "You must include a valid hash");
            }
            if (string.IsNullOrEmpty(input))
            {
                input = "ThisIsWorking=sha512";
            }

            var shaUtil = new Sha512Util(hashKey);
            return Request.CreateResponse(shaUtil.GetEncryptedString(input));

        }

        [Route("api/util/sha512/{input}")]
        [Route("api/util/sha512/v={input}")]
        public HttpResponseMessage GetSha512Encrypted(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                input = "ThisIsWorking=sha512";
            }

            return Request.CreateResponse(GetSha512Response(input));
        }

        [Route("api/util/pgp/{input}")]
        public HttpResponseMessage GetPgpEncrypted(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                input = "ThisIsWorking=pgp";
            }

            return Request.CreateResponse(GetPgpResponse(input));
        }

        public string GetSha512Response(string input)
        {
            return _shaUtil.GetEncryptedString(input);
        }

        public string GetPgpResponse(string input)
        {
            return _pgpUtil.GetEncryptedString(input);
        }

    }

    public class ChatWebSocketHandler : WebSocketHandler
    {
        private static readonly WebSocketCollection _chatClients = new WebSocketCollection();
        private UtilController _util;

        public ChatWebSocketHandler(UtilController util)
        {
            _util = util;
        }

        public override void OnOpen()
        {
            //base.OnOpen();
            _chatClients.Add(this);
            _chatClients.Broadcast("We've connected");
        }

        public override void OnMessage(string message)
        {
            //base.OnMessage(message);
            //_chatClients.Broadcast("message: " + message);
            if (_util != null)
            {
                if (message.Contains("sha512:"))
                {
                    _chatClients.Broadcast(
                        _util.GetSha512Response(
                            message.Substring(message.IndexOf("sha512:", StringComparison.OrdinalIgnoreCase) + 7)));
                }
                else if (message.Contains("pgp:"))
                {
                    _chatClients.Broadcast(
                        _util.GetPgpResponse(
                            message.Substring(message.IndexOf("pgp:", StringComparison.OrdinalIgnoreCase) + 4)));
                }
                else
                {
                    _chatClients.Broadcast("Not a valid websocket message");
                }
            }
            else
            {
                throw new Exception("Not a valid constructor has been created");
            }
        }

        public override void OnError()
        {
            _chatClients.Broadcast("An error occurred");
        }

        public override void OnClose()
        {
            _chatClients.Remove(this);
        }
    }

}
