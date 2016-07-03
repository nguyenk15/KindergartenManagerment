Về record status: 
-Khi thêm dòng mới =>set record status = 1
Về vấn đề xóa:
-Đối với dòng đã được duyệt =>set record status = 0
-Đối với dòng chưa được duyệt =>xóa hẳn khỏi CSDL
Cả 2 vấn đề đều có 2 giải pháp:
-Giải pháp 1: viết trigger trong CSDL
-Giải pháp 2: xử lý trong code controller
=> Nhóm cần thảo luận chọn phương pháp phù hợp, dễ cho việc bảo trì sửa chữa nhất