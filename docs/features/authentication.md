# Authorization

## 1. Mục tiêu
Phân quyền người dùng dựa theo Roles.

## 2. Các thành phần
- PermissionRequirement
- PermissionAuthorizationHandler
- Custom Authorization Policy
- JWT Claims

## 3. Luồng xử lý
1. User gửi request kèm JWT.
2. Request đi qua API Gateway.
2. Middleware API Gateway xác thực JWT.
3. Authorization đọc Roles từ Claims.
4. Handler kết hợp Redis kiểm tra quyền truy cập.
5. Cho phép hoặc từ chối request.

                  ┌─────────────────┐
                  │ IdentityService │
                  │                 │
                  │ User            │
                  │ Role            │
                  │ Permission      │
                  └────────┬────────┘
                           │
                     Permission query
                           │
                           ▼
                       ┌───────┐
                       │ Redis │
                       └───┬───┘
                           │
Client ── JWT ──► API Gateway
                     │
                     │ Roles
                     ▼
               Permission Cache
                     │
              ┌──────┴──────┐
              │             │
            HIT           MISS
              │             │
              │             ▼
              │      IdentityService
              │             │
              │             ▼
              │           Redis
              │
              ▼
        Authorization
              │
              ▼
            YARP
              │
              ▼
         PostService

## 4. Cách triển khai
### Bước 1: Tạo API lấy Permission theo Role
Tạo API lấy Permission theo Role ở identity-service

### Bước 1: Tạo PermissionAuthorizationRequirement
Tạo class PermissionAuthorizationRequirement kế thừa interface IAuthorizationRequirement.

### Bước 2: Tạo AuthorizationHandler
Tạo class PermissionAuthorizationHandler kế thừa AuthorizationHandler và nhận vào 1 requirement là PermissionAuthorizationRequirement. Override HandleRequirementAsync để thực hiện kiểm tra và xử lý requirement.

### Bước 3: Đăng ký Authorization
Khai báo DI Singleton cho PermissionAuthorizationHandler, cấu hình DefaultPolicy cho Authorize sử dụng PermissionAuthorizationRequirement nếu muốn sử dụng lại [Authorize] mà không cần khai báo chi tiết (Khi không cấu hình DefaultPolicy: [Authorize(Policy = "Permission")]). Khai báo UseAuthorization sau UseAuthentication.