using SignalRSqlTableDependency.Extension;
using SignalRSqlTableDependency.SignalRHub;
using SignalRSqlTableDependency.SubscribeTableDependencies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use for signalr db trigger
builder.Services.AddSingleton<ChatHub>();
builder.Services.AddSingleton<SubscribeProductsTableDependency>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseHttpsRedirection();
}   

app.UseCors(x => x.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .WithOrigins(builder.Configuration["CorsSettings:Angular"])
                );
//app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

// Use for signalr db trigger
await app.UseSqlTableDependency<SubscribeProductsTableDependency>(builder.Configuration["DatabaseSettings:ConnectionString"]);

app.Run();
