using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SMSChat.Client;
using SMSChat.Client.Services;
using SMSChat.Client.WebRtc;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
builder.Services.AddScoped<WebRtcService>();
builder.Services.AddScoped<SmsService>();
//string bearerToken = "S0hCVVhjY25WS3dUUTRuL0hLT0tEUUVkMnczWTFJSW8vamtNMWhlcXN6TT0="; // Retrieve this securely

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://voip.ms/api/v1/rest.php") });
//builder.Services.AddScoped(sp => new VoipMsSmsService(sp.GetRequiredService<HttpClient>(), bearerToken));



await builder.Build().RunAsync();
