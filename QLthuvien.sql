create database QLthuvien
go
use QLthuvien
go

INSERT INTO Authors (FullName) VALUES
(N'Nguyễn Nhật Ánh'),
(N'J.K. Rowling'),
(N'Haruki Murakami'),
(N'George Orwell'),
(N'Ernest Hemingway');
go
INSERT INTO Publishers (Name) VALUES
(N'NXB Trẻ'),             -- Id = 1
(N'NXB Kim Đồng'),        -- Id = 2
(N'Bloomsbury Publishing'), -- Id = 3
(N'Secker & Warburg'),    -- Id = 4
(N'Vintage International'); -- Id = 5
go
INSERT INTO Books (Title, Description, IsRead, DateRead, Rate, Genre, CoverUrl, DateAdded, PublisherID) VALUES
(N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', N'Truyện tuổi thơ trong sáng tại vùng quê Việt Nam', 1, '2023-05-12', 9, N'Truyện dài', N'/images/hoavang.jpg', '2023-01-01', 1),
(N'Harry Potter and the Philosopher''s Stone', N'Phần đầu tiên trong loạt truyện Harry Potter', 1, '2022-11-01', 10, N'Fantasy', N'/images/hp1.jpg', '2022-05-10', 3),
(N'1Q84', N'Tiểu thuyết nổi tiếng của Haruki Murakami', 0, NULL, NULL, N'Tiểu thuyết', N'/images/1q84.jpg', '2023-07-15', 5),
(N'1984', N'Tác phẩm kinh điển phản địa đàng', 1, '2023-02-20', 9, N'Dystopia', N'/images/1984.jpg', '2023-01-10', 4),
(N'The Old Man and the Sea', N'Câu chuyện cảm động về ông lão và biển cả', 1, '2021-09-18', 8, N'Truyện ngắn', N'/images/oldmansea.jpg', '2021-05-20', 5);
go
INSERT INTO Publishers (Name) VALUES
(N'NXB Trẻ'),
(N'NXB Kim Đồng'),
(N'Bloomsbury'),
(N'NXB Hội Nhà Văn'),
(N'Vintage International');
go