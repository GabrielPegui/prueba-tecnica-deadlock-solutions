namespace Prueba.Services;
public record ServiceResult<T>(bool Success, T? Value, string? Error, int StatusCode) { public static ServiceResult<T> Ok(T v) => new(true, v, null, 200); public static ServiceResult<T> Created(T v) => new(true, v, null, 201); public static ServiceResult<T> Fail(string e, int s) => new(false, default, e, s); }
