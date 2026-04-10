using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SumoSushiApp_BackEnd.Controllers.Utils;
using SumoSushiApp_BackEnd.Data;
using SumoSushiApp_BackEnd.Hubs;
using SumoSushiApp_BackEnd.Models;
using SumoSushiApp_BackEnd.Repositories;
using SumoSushiApp_BackEnd.Repositories.Interfaces;
using SumoSushiApp_BackEnd.Services;
using SumoSushiApp_BackEnd.Services.Interfaces;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
//builder.Services.AddSingleton<PushNotificationService>();


#region Inyeccion de Dependencias
// ===================================================
// INYECCION DE DEPENDENCIAS
// ===================================================
builder.Services.AddScoped<IDBContext, DBContext>();
builder.Services.AddScoped<DBContext>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IHashService, HashService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ITelefonoRepository, TelefonoRepository>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IDireccionRepository, DireccionRepository>();
builder.Services.AddScoped<IDireccionService,DireccionService>();
builder.Services.AddScoped<ICorreoService, CorreoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IRestauranteRepository, RestauranteRepository>();
builder.Services.AddScoped<IRestauranteService, RestauranteService>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<IImgService, ImgService>();
builder.Services.AddScoped<IPromocionRepository, PromocionRepository>();
builder.Services.AddScoped<IPromocionService, PromocionService>();
builder.Services.AddScoped<ControllerUtils>();
builder.Services.AddScoped<IRolRepository, RolRepository>();
builder.Services.AddScoped<IRolService, RolService>();
builder.Services.AddScoped<ITelefonoService, TelefonoService>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriasRepositoty>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<ICategoriaRepository, CategoriasRepositoty>();
builder.Services.AddScoped<ICategoriaRepository, CategoriasRepositoty>();
builder.Services.AddScoped<ICategoriaService, CategoriaService>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IEtiquetaRepository, EtiquetaRepository>();
builder.Services.AddScoped<IEtiquetaService, EtiquetaService>();
builder.Services.AddScoped<IPresentacionRepository, PresentacionRepository>();
builder.Services.AddScoped<IPresentacionService, PresentacionService>();
builder.Services.AddScoped<IPuntosRepository, PuntosRepository>();
builder.Services.AddScoped<IPuntosService, PuntosService>();
builder.Services.AddScoped<ICarritoRepository, CarritoRepository>();
builder.Services.AddScoped<ICarritoService, CarritoService>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IPushNotificationService, PushNotificationService>();
#endregion


#region Inyeccion de Configuraciones
// ===================================================
// INYECCION DE APPSETTINGS.json
// ===================================================
builder.Services.Configure<CorreoConfig>(builder.Configuration.GetSection("CorreoConfig"));

#endregion

#region Configuracion de aplicacion
// ===================================================
// CUSTOMIZACION DE SWAGGER PARA AUTENTICACION JWT
// ===================================================
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

// ===================================================
// CONFIGURACION DE JWT
// ===================================================
string SecretKey = builder.Configuration.GetSection("JWT:LlaveToken").Value!;
string Issuer = builder.Configuration.GetSection("JWT:Issuer").Value!;
string Audience = builder.Configuration.GetSection("JWT:Audience").Value!;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),

        ValidateIssuer = true,
        ValidIssuer = Issuer,

        ValidateAudience = true,
        ValidAudience = Audience,

        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,

        NameClaimType = "IdUsuario", 
        RoleClaimType = "Rol" // Esto permite usar [Authorize(Roles = "Admin")] y similares
    };
    // Esto permite tomar el token desde query string en SignalR
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];

            // Si la petición es para el Hub de SignalR
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) &&
                (path.StartsWithSegments("/hubs/pedidos")))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4173") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler("/api/Error/CapturarError");

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.MapHub<PedidoHub>("/hubs/pedidos");

app.Run();