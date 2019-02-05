﻿using AngleSharp.Html.Dom;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace TestesDeIntegracao.Helpers
{
    public static class HttpClientExtensions
    {
        public static Task<HttpResponseMessage> SendAsync(
            this HttpClient client,
            IHtmlFormElement form,
            IHtmlElement submitButton)
        {
            return client.SendAsync(form, submitButton, new Dictionary<string, string>());
        }

        public static Task<HttpResponseMessage> SendAsync(this HttpClient client,
            IHtmlFormElement form, IEnumerable<KeyValuePair<string, string>> formValues)
        {
            var submitElement = form.QuerySelectorAll("[type=submit]").Single();
            var submitButton = (IHtmlElement)submitElement;
            return client.SendAsync(form, submitButton, formValues);
        }

        public static Task<HttpResponseMessage> SendAsync(this HttpClient client,
            IHtmlFormElement form, IHtmlElement submitButton, IEnumerable<KeyValuePair<string, string>> formValues)
        {
            foreach (var kvp in formValues)
            {
                var element = (IHtmlInputElement)form[kvp.Key];
                element.Value = kvp.Value;
            }

            var submit = form.GetSubmission(submitButton);
            var target = (Uri)submit.Target;
            if (submitButton.HasAttribute("formaction"))
            {
                var formaction = submitButton.GetAttribute("formaction");
                target = new Uri(formaction, UriKind.Relative);
            }
            var submision = new HttpRequestMessage(new HttpMethod(submit.Method.ToString()), target)
            {
                Content = new StreamContent(submit.Body)
            };

            foreach (var header in submit.Headers)
            {
                submision.Headers.TryAddWithoutValidation(header.Key, header.Value);
                submision.Content.Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            return client.SendAsync(submision);
        }
    }
}
