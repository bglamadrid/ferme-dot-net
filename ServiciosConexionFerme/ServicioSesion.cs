﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Biblioteca;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ServiciosConexionFerme
{
    public class ServicioSesion
    {
        public Sesion ConexionLogin(LoginRequest log)
        {
            var json = JsonConvert.SerializeObject(log);
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlConexion.url);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent jsonp = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("sesiones/abrir", jsonp);
            var resp = responseMessage.Result.Content.ReadAsStringAsync().Result;

            var JsonSerializerSettings = new JsonSerializerSettings();
            JsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;

            if (resp != null && resp != "")
            {
                Sesion LoginResp = JsonConvert.DeserializeObject<Sesion>(resp,JsonSerializerSettings);
                return LoginResp;
            }
            else
            {
               return null;
            }
           
        }

        public bool ValidaConexion(Sesion se)
        {
            var json = JsonConvert.SerializeObject(se);
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlConexion.url);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent jsonp = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("sesiones/validar", jsonp);
            var resp = responseMessage.Result.Content.ReadAsStringAsync().Result;

            return (resp == "true");
           
        }


        public bool CerrarConexion(Sesion se)
        {
            var json = JsonConvert.SerializeObject(se);
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(UrlConexion.url);
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            System.Net.Http.HttpContent jsonp = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var responseMessage = httpClient.PostAsync("sesiones/cerrar", jsonp);
            var resp = responseMessage.Result.Content.ReadAsStringAsync().Result;

            return (resp == "true");

        }







    }
}