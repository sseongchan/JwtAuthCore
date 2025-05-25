using JwtAuthSample.DI;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// ✅ Dapper 설정
DapperConfig.ConfigureDapper();

// ✅ Razor View 포함 (Swagger UI 위해 필요)
builder.Services.AddControllersWithViews();

// ✅ 서비스 등록
builder.Services
    .AddJwtAuth(builder.Configuration)
    .AddAppPolicy()
    .AddAppCors(builder.Configuration)
    .AddAppSwagger()
    .AddOracleDb(builder.Configuration)
    .AddAppServices();

// ✅ 반드시 미들웨어 순서 주의
var app = builder.Build();

app.UseRouting();               // ❗ CORS 이전에 라우팅 등록
app.UseCors();                  // ✅ 이제 CORS가 안전하게 작동
app.UseAuthentication();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
