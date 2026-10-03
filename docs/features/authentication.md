# Authorization

## 1. Mục tiêu
Phân quyền người dùng dựa trên Permission lưu trong JWT.

## 2. Các thành phần
- PermissionRequirement
- PermissionAuthorizationHandler
- Custom Authorization Policy
- JWT Claims

## 3. Luồng xử lý
1. User gửi request kèm JWT.
2. Middleware xác thực JWT.
3. Authorization đọc Permission từ Claims.
4. Handler kiểm tra quyền truy cập.
5. Cho phép hoặc từ chối request.

## 4. Cách triển khai
### Bước 1: Tạo PermissionAuthorizationRequirement
Tạo class PermissionAuthorizationRequirement kế thừa interface IAuthorizationRequirement.

### Bước 2: Tạo AuthorizationHandler
Tạo class PermissionAuthorizationHandler kế thừa AuthorizationHandler và nhận vào 1 requirement là PermissionAuthorizationRequirement. Override HandleRequirementAsync để thực hiện kiểm tra và xử lý requirement.

### Bước 3: Đăng ký Authorization
Khai báo DI Singleton cho PermissionAuthorizationHandler, cấu hình DefaultPolicy cho Authorize sử dụng PermissionAuthorizationRequirement nếu muốn sử dụng lại [Authorize] mà không cần khai báo chi tiết (Khi không cấu hình DefaultPolicy: [Authorize(Policy = "Permission")]). Khai báo UseAuthorization sau UseAuthentication.