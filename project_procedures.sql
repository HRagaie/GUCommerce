--1
CREATE PROC customerRegister
@username VARCHAR(20),
@first_name VARCHAR(20),
@last_name VARCHAR(20),
@password VARCHAR(20),
@email VARCHAR(50)

AS

INSERT INTO Users VALUES (@username, @password, @first_name, @last_name, @email)
INSERT INTO Customer(username) VALUES (@username)
--2
CREATE PROC vendorRegister
@username VARCHAR(20),
@first_name VARCHAR(20),
@last_name VARCHAR(20),
@password VARCHAR(20),
@email VARCHAR(50),
@company_name VARCHAR(20),
@bank_acc_no VARCHAR(20)

AS
INSERT INTO Users VALUES (@username, @password, @first_name, @last_name, @email)
INSERT INTO Vendor (username, company_name, bank_acc_no) VALUES (@username, @company_name, @bank_acc_no)
--3
CREATE   PROC userLogin
@username VARCHAR(20),
@password VARCHAR(20),
@success BIT OUTPUT,
@type INT OUTPUT
AS

DECLARE @test int
SELECT @test= count(c.username) 
FROM Customer c 
INNER JOIN Users u ON c.username = u.username
WHERE c.username = @username AND u.password = @password


IF @test <> 0
	BEGIN
	SET @success = '1' 
	SET @type = 0
	print 'success -> ' + CAST(@success AS varchar) + ' type -> ' + CAST(@type AS varchar)
	END
ELSE
	BEGIN
		SELECT @test= count(v.username) 
		FROM Vendor v 
		INNER JOIN Users u ON v.username = u.username
		WHERE v.username = @username AND u.password = @password
		IF @test <> 0
			BEGIN
			SET @success = '1' 
			SET @type = 1
			print 'success -> ' + CAST(@success AS varchar) + ' type -> ' + CAST(@type AS varchar)
		END
	
	ELSE
		BEGIN
		SELECT @test= count(a.username) 
		FROM Admins a 
		INNER JOIN Users u ON a.username = u.username
		WHERE a.username = @username AND u.password = @password
		
		IF @test <> 0
			BEGIN
			SET @success = '1' 
			SET @type = 2
			print 'success -> ' + CAST(@success AS varchar) + ' type -> ' + CAST(@type AS varchar)
			END
		ELSE	
			BEGIN
			SELECT @test= count(d.username) 
			FROM Delivery_Person d 
			INNER JOIN Users u ON d.username = u.username
			WHERE d.username = @username AND u.password = @password
			IF @test <> 0
				BEGIN
				SET @success = '1' 
				SET @type = 3
				print 'success -> ' + CAST(@success AS varchar) + ' type -> ' + CAST(@type AS varchar)
				END
			ELSE
				BEGIN
				SET @success = '0' 
				SET @type = -1
				print 'success -> ' + CAST(@success AS varchar) + ' type -> ' + CAST(@type AS varchar)
				END
			END
		END
	END
--4
CREATE PROC addMobile
@username VARCHAR(20),
@mobile_number VARCHAR(20)
AS
INSERT INTO user_mobile_number VALUES (@mobile_number,@username)

--5
CREATE PROC addAddress
@username VARCHAR(20),
@address VARCHAR(100)
AS
INSERT INTO User_addresses VALUES (@address,@username)

--6
CREATE PROC showProducts 
AS
SELECT p.product_name,p.product_description,p.price,p.final_price,p.color FROM Product p
WHERE p.available = '1'

--7
CREATE PROC showProductsbyPrice
AS
SELECT p.serial_no,p.product_name,p.product_description,p.price,p.final_price,p.color FROM Product p
WHERE p.available = '1'
ORDER BY p.price

--8
CREATE   PROC searchbyname
@text varchar(20)
AS
SELECT p.product_name,p.product_description,p.price,p.final_price,p.color FROM Product p
WHERE p.available = '1' AND  p.product_name LIKE '%'+@text+'%'

--9
CREATE PROC AddQuestion
@serial int,
@customer varchar(20),
@question varchar(50)
AS
INSERT INTO Customer_Question_Product (serial_no,customer_name,question) VALUES (@serial,@customer,@question)

--10
CREATE PROC AddToCart
@customername varchar(20),
@serial int
AS
DECLARE @test int
SELECT @test=count(p.serial_no) FROM Product p WHERE p.serial_no=@serial AND p.available = '1'
IF @test <>0
	BEGIN
	INSERT INTO CutomerAddstoCartProduct(serial_no,customer_name) VALUES (@serial,@customername)
	END
ELSE
	BEGIN
	print 'product is already unavailable'
	END
--11
CREATE PROC removefromCart
@customername varchar(20),
@serial int,
@success bit OUTPUT
AS
IF EXISTS(SELECT * FROM CutomerAddstoCartProduct WHERE serial_no = @serial AND customer_name = @customername)
BEGIN
DELETE FROM CutomerAddstoCartProduct
WHERE serial_no = @serial AND customer_name = @customername
SET @success='1'
END
ELSE
BEGIN
SET @success='0'
END

--12
CREATE PROC createWishlist
@customername varchar(20),
@name varchar(20)
AS
INSERT INTO Wishlist VALUES (@customername,@name)

--13
CREATE PROC AddtoWishlist
@customername varchar(20),
@wishlistname varchar(20),
@serial int
AS
INSERT INTO Wishlist_Product VALUES (@customername,@wishlistname,@serial)

--14
CREATE PROC removefromWishlist
@customername varchar(20),
@wishlistname varchar(20),
@serial int,
@success bit OUTPUT
AS 
IF EXISTS(SELECT * FROM Wishlist_Product WHERE username = @customername AND wish_name = @wishlistname AND serial_no = @serial) 
BEGIN
DELETE FROM Wishlist_Product 
WHERE username = @customername AND wish_name = @wishlistname AND serial_no = @serial
SET @success='1'
END
ELSE
BEGIN
SET @success='0'
END

--15
CREATE PROC showWishlistProduct
@customername varchar(20),
@name varchar(20)
AS
SELECT p.serial_no, p.product_name,p.product_description,p.price,p.final_price,p.color 
FROM Product p 
INNER JOIN Wishlist_Product w 
ON w.serial_no=p.serial_no
WHERE w.username=@customername AND w.wish_name= @name

--16
CREATE PROC viewMyCart
@customer varchar(20)
AS
SELECT p.serial_no,p.product_name,p.product_description,p.price,p.final_price,p.color 
FROM Product p 
INNER JOIN CutomerAddstoCartProduct c 
ON c.serial_no=p.serial_no
WHERE c.customer_name=@customer

--17
CREATE PROC calculatepriceOrder
@customername varchar(20),
@sum decimal(10,2) OUTPUT
AS
SELECT @sum=SUM(p.final_price)
FROM Product p
INNER JOIN CutomerAddstoCartProduct c
ON p.serial_no=c.serial_no
WHERE c.customer_name=@customername

print @sum

--18
CREATE PROC productsinorder
@customername varchar(20),
@orderID int
AS
UPDATE p
SET p.available = '0' , p.customer_order_id = @orderID , p.customer_username = @customername
FROM CutomerAddstoCartProduct c
INNER JOIN Product p ON p.serial_no = c.serial_no
WHERE c.customer_name = @customername

DELETE cp
FROM CutomerAddstoCartProduct cp
INNER JOIN Product p ON p.serial_no = cp.serial_no
WHERE p.available= '0'

--19
CREATE PROC emptyCart
@customername varchar(20)
AS
DELETE cp
FROM CutomerAddstoCartProduct cp
WHERE cp.customer_name = @customername

--20
CREATE PROC makeOrder
@customername varchar(20)
AS
DECLARE @sum decimal(10,2)
DECLARE @existflag int 
SELECT @existflag = count(c.customer_name) FROM CutomerAddstoCartProduct c WHERE c.customer_name = @customername
IF @existflag <> 0
BEGIN
EXEC calculatepriceOrder @customername, @sum OUTPUT
INSERT INTO Orders (customer_name,order_status,order_date,total_amount) VALUES (@customername, 'not processed',GETDATE(), @sum)

DECLARE @orderID int
SELECT @orderID=order_no
FROM Orders
WHERE order_no = IDENT_CURRENT('Orders')


EXEC productsinorder @customername, @orderID
EXEC emptyCart @customername
END
ELSE
BEGIN
print 'You have nothing in cart'
END
SELECT o.order_no,o.total_amount FROM Orders o WHERE  @orderID=order_no
--21
CREATE PROC SpecifyAmount
@customername varchar(20),
@orderID int,
@cash decimal(10,2),
@credit decimal(10,2)
AS

If @cash is null AND @credit IS NOT NULL
	BEGIN
	SET @cash = 0
	UPDATE Orders
	SET payment_type = 'credit'
	WHERE order_no = @orderID
	END
ELSE

IF @credit is null AND @cash IS NOT NULL
	BEGIN
	SET @credit = 0 
	UPDATE Orders
	SET payment_type = 'cash'
	WHERE order_no = @orderID
	END

UPDATE Orders
SET cash_amount = @cash, credit_amount = @credit
WHERE order_no=@orderID AND customer_name=@customername

DECLARE @totalamount decimal(10,2)
SELECT @totalamount = o.total_amount 
FROM Orders o
WHERE o.order_no = @orderID

DECLARE @points decimal(10,2)
SET @points = @totalamount - @cash - @credit



DECLARE @remainingpoints decimal(10,2), @code varchar(10), @customerpoints int
If @points <> 0
	BEGIN
		
		SELECT @remainingpoints = a.remaining_points, @code = a.code
		FROM Admin_Customer_Giftcard a
		WHERE a.customer_name = @customername

		IF @credit is null AND @cash is null AND @code is not null 
		BEGIN
		SET @credit = 0
		SET @cash = 0
		UPDATE Orders
		SET payment_type = 'points'
		WHERE order_no = @orderID
		END

		UPDATE Admin_Customer_Giftcard
		SET remaining_points = @remainingpoints - @points
		WHERE customer_name = @customername

		SELECT @customerpoints = c.points
		FROM Customer c
		WHERE username = @customername

		UPDATE Customer
		SET points = @customerpoints - @points
		WHERE username = @customername
		
		UPDATE Orders
		SET  Gift_Card_code_used = @code
		WHERE order_no=@orderID AND customer_name=@customername
	END

--22
CREATE   PROC recommend
@customername varchar(20)
AS
SELECT * from (
SELECT serial_no, product_name, category, product_description, price, final_price, ROW_NUMBER() OVER ( ORDER BY PROD_CNT DESC ) rownum 
FROM (
SELECT p.serial_no, p.product_name, p.category, p.product_description, p.price, p.final_price, count(1) PROD_CNT
  FROM Wishlist_Product w
  INNER JOIN Product p ON w.serial_no=p.serial_no
  WHERE p.serial_no not in (SELECT cart1.serial_no FROM CutomerAddstoCartProduct cart1 where cart1.customer_name =  @customername)
    and p.serial_no not in (SELECT wish1.serial_no FROM Wishlist_Product wish1 where wish1.username = @customername)
    and p.category in (
                         SELECT b.CATEGORY 
						   from ( select a.CATEGORY, a.CNT, ROW_NUMBER() OVER ( ORDER BY CNT DESC ) rownum 
                                    from ( SELECT p.category, count(1) CNT
	                                        FROM CutomerAddstoCartProduct c
	                                        INNER JOIN Product p ON c.serial_no = p.serial_no
	                                        WHERE c.customer_name = @customername
	                                        GROUP BY p.category
	                                      ) a
                                ) b
                          WHERE b.rownum < 4
                       )
GROUP BY p.serial_no, p.product_name, p.category, p.product_description, p.price, p.final_price
) d
) e
WHERE e.rownum < 4

UNION 

SELECT * FROM (
SELECT serial_no, product_name, category, product_description, price, final_price, ROW_NUMBER() OVER ( ORDER BY CNT DESC ) rownum 
FROM (
SELECT p.serial_no, p.product_name, p.category, p.product_description, p.price, p.final_price, count(1) CNT
  FROM Wishlist_Product w
  INNER JOIN Product p ON w.serial_no=p.serial_no
  WHERE p.serial_no not in (SELECT cart1.serial_no FROM CutomerAddstoCartProduct cart1 where cart1.customer_name =  @customername)
    and p.serial_no not in (SELECT  wish1.serial_no FROM Wishlist_Product wish1 where wish1.username =  @customername)
    and 
	w.username in ( SELECT cart.customer_name
	                   FROM CutomerAddstoCartProduct cart
					  WHERE cart.serial_no in (
                                          SELECT p.serial_no
	                                        FROM CutomerAddstoCartProduct c
	                                        INNER JOIN Product p ON c.serial_no = p.serial_no
	                                        WHERE c.customer_name = @customername 
											)
                       )
GROUP BY p.serial_no, p.product_name, p.category, p.product_description, p.price, p.final_price) a
) b
where b.rownum < 4

--23
CREATE   PROC cancelOrder
@orderID int
AS

DECLARE @cash_amount decimal(10,2), @credit_amounts decimal(10,2),@points int, @total_amount decimal(10,2), @giftcard varchar(10), @check int, @customername varchar(20)
SELECT @check = count(1)
FROM Orders o
WHERE order_no = @orderID AND (order_status <> 'not processed' OR order_status <> 'in process')


IF @check <> 0 
	SELECT @cash_amount = o.cash_amount, @credit_amounts = o.credit_amount, @total_amount=o.total_amount, @giftcard =o.Gift_Card_code_used, @customername =customer_name
	FROM Orders o
	WHERE order_no = @orderID AND (order_status <> 'not processed' OR order_status <> 'in process')
	BEGIN
		IF @giftcard is not null
			BEGIN
				DECLARE @check2 int
				SELECT @check2=count(1) FROM Giftcard WHERE code = @giftcard AND expiry_date >CURRENT_TIMESTAMP
				IF @check2<> 0
					BEGIN
						SET @points=@total_amount-@cash_amount-@credit_amounts
						UPDATE Admin_Customer_Giftcard
						SET remaining_points = remaining_points + @points
						WHERE customer_name = @customername AND code = @giftcard
						UPDATE Customer	
						SET points = points + @points
						WHERE username = @customername
					END
			END

				UPDATE p 
				SET p.available = '1',p.customer_order_id = null, p.customer_username=null
				FROM Product p
				WHERE p.customer_order_id = @orderID

				DELETE FROM Orders
				WHERE order_no= @orderID 
			
	END
--24
CREATE PROC returnProduct
@serialno int,
@orderID int
AS
DECLARE @finalprice decimal(10,2)
SELECT @finalprice=p.final_price 
From Product p
WHERE serial_no = @serialno AND customer_order_id = @orderID

UPDATE Product 
SET available = '1' , customer_order_id = NULL, customer_username= NULL
WHERE serial_no = @serialno AND customer_order_id = @orderID

DECLARE @cash_amount int, @credit_amounts int,@points int, @total_amount int, @giftcard varchar(10), @customername varchar(20)
SELECT  @cash_amount = o.cash_amount, @credit_amounts = o.credit_amount, @total_amount=o.total_amount, @giftcard =o.Gift_Card_code_used, @customername = customer_name
FROM Orders o
WHERE order_no = @orderID

DECLARE @check int
SELECT @check=count(1) FROM Giftcard WHERE code = @giftcard AND expiry_date >CURRENT_TIMESTAMP

IF @giftcard is not null AND @check<> 0
BEGIN
	SET @points=@total_amount-@cash_amount-@credit_amounts
	IF @finalprice < @points
	BEGIN
		UPDATE Admin_Customer_Giftcard
		SET remaining_points = remaining_points + @finalprice
		WHERE customer_name = @customername AND code = @giftcard
		UPDATE Customer
		SET points = points + @finalprice
		WHERE username = @customername
	END
	ELSE
	BEGIN
		UPDATE Admin_Customer_Giftcard
		SET remaining_points = remaining_points + @points
		WHERE customer_name = @customername AND code = @giftcard
		UPDATE Customer
		SET points = points + @points
		WHERE username = @customername
	END
END

UPDATE Orders
SET total_amount = total_amount - @finalprice
WHERE order_no = @orderID
--25
CREATE PROC showproductsIbought
@customername varchar(20)
AS
SELECT p.serial_no,p.product_name,p.product_description,p.category, p.price,p.final_price,p.color FROM Product p
WHERE p.customer_username = @customername
--26
CREATE PROC rate
@serialno int,
@rate int,
@customername varchar(20)
AS
UPDATE Product
SET rate = @rate
WHERE customer_username = @customername AND serial_no = @serialno

SELECT p.serial_no,p.product_name,p.product_description,p.category, p.price,p.final_price,p.color,p.available,p.rate 
FROM Product p
WHERE customer_username = @customername AND serial_no = @serialno
--27
CREATE PROC AddCreditCard
@creditcardnumber varchar(20),
@expirydate date,
@cvv varchar(4),
@customername varchar(20)
AS
INSERT INTO Credit_Card VALUES (@creditcardnumber,@expirydate,@cvv)
INSERT INTO Customer_CreditCard VALUES(@customername,@creditcardnumber)
--28
CREATE PROC ChooseCreditCard
@creditcardnumber varchar(20),
@orderid int
AS
UPDATE Orders
SET creditCard_number = @creditcardnumber
WHERE order_no = @orderid
--29
CREATE PROC viewDeliveryTypes
AS
SELECT d.type,d.time_duration,d.fees
FROM Delivery d
--30
CREATE PROC specifydeliverytype
@orderID int,
@deliveryID int
AS
UPDATE Orders
SET delivery_id = @deliveryID
WHERE order_no = @orderID

UPDATE o
SET o.remaining_days = d.time_duration - (DATEDIFF(day, o.order_date, GETDATE()))
FROM Orders o 
INNER JOIN Delivery d 
ON o.delivery_id = d.id
WHERE o.order_no = @orderID
--31
CREATE PROC trackRemainingDays
@orderID int,
@customername varchar(20),
@days int OUTPUT
AS
SELECT @days = d.time_duration - (DATEDIFF(day, o.order_date, GETDATE()))
FROM Orders o 
INNER JOIN Delivery d 
ON o.delivery_id = d.id
WHERE o.order_no = @orderID

PRINT @days

-----------------------------ADMINS

--1

CREATE PROC activateVendors

@admin_username VARCHAR(20),
@vendor_username VARCHAR(20)
AS

UPDATE v
SET v.admin_username = @admin_username, v.activated ='1' 
FROM Vendor v
WHERE v.username = @vendor_username

--2
CREATE PROC inviteDeliveryPerson

@delivery_username VARCHAR(20),
@delivery_email VARCHAR(50)
AS

INSERT INTO Users (username, email)
VALUES(@delivery_username, @delivery_email);

INSERT INTO Delivery_Person VALUES ('0', @delivery_username)

--3
CREATE PROC reviewOrders
AS
SELECT *
FROM Orders

--4
CREATE PROC updateOrderStatusInProcess
@order_no INT
AS

UPDATE o
SET o.order_status = 'in process'
FROM Orders o
WHERE o.order_no = @order_no

--5

CREATE PROC addDelivery
@delivery_type VARCHAR(20),
@time_duration INT,
@fees DECIMAL(5,3),
@admin_username VARCHAR(20)

AS

INSERT INTO Delivery (type, time_duration ,fees , username)
VALUES( @delivery_type, @time_duration, @fees, @admin_username)

--6
CREATE PROC assignOrderToDelivery

@delivery_username VARCHAR(20),
@order_no INT,
@admin_username VARCHAR(20)
AS

INSERT INTO Admin_Delivery_Order (delivery_username, order_no, admin_username)
VALUES( @delivery_username, @order_no, @admin_username)

--7

CREATE PROC createTodaysDeal
@deal_amount INT,
@admin_username VARCHAR(20),
@expiry_date DATETIME
AS

INSERT INTO Todays_Deals ( deal_amount, admin_username, expiry_date)
VALUES( @deal_amount, @admin_username, @expiry_date)


--8

CREATE PROC checkTodaysDealOnProduct
@serial_no INT,
@activeDeal BIT OUTPUT
AS

DECLARE @deal_count INT

SELECT  @deal_count = COUNT(serial_no)
FROM Todays_Deals_Product
WHERE serial_no = @serial_no

IF @deal_count <> 0
BEGIN
 SET @activeDeal = '1'
END

ELSE
BEGIN
SET @activeDeal = '0'
END

PRINT @activeDeal

--9
CREATE   PROC addTodaysDealOnProduct
@deal_id INT, 
@serial_no INT

AS
DECLARE @deal_amount INT
DECLARE @check int
SELECT @check=count(1) FROM Todays_Deals t WHERE t.deal_id = @deal_id AND t.expiry_date>CURRENT_TIMESTAMP
IF @check <>0
BEGIN
    INSERT INTO Todays_Deals_Product VALUES (@deal_id, @serial_no)

    SELECT @deal_amount = t.deal_amount
    FROM Todays_Deals t
    WHERE t.deal_id = @deal_id

    UPDATE p
    SET p.final_price = (p.price -(p.price * @deal_amount/100) ) 
    FROM Product p
    WHERE p.serial_no = @serial_no
END
--10

CREATE   PROC removeExpiredDeal
@deal_id INT

AS
DECLARE @expiry_date DATETIME
DECLARE @activeDeal BIT 
DECLARE @deal_amount INT
DECLARE @serial_no INT

SELECT  @expiry_date = t.expiry_date
FROM Todays_Deals t
WHERE t.deal_id = @deal_id
IF (Current_timestamp > @expiry_date)
		BEGIN

				IF EXISTS(SELECT * FROM Todays_Deals_Product t WHERE t.deal_id=@deal_id)
				BEGIN
						SELECT @serial_no=t.serial_no 
						FROM Todays_Deals_Product t 
						WHERE t.deal_id=@deal_id

						SELECT @deal_amount = t.deal_amount
						FROM Todays_Deals t
						WHERE t.deal_id = @deal_id

						UPDATE p
						SET p.final_price = p.price
						FROM Product p
						WHERE p.serial_no = @serial_no

						DELETE
						FROM Todays_Deals_Product
						WHERE deal_id = @deal_id

						DELETE
						FROM Todays_Deals
						WHERE deal_id = @deal_id
				END
				ELSE
				BEGIN
						DELETE
						FROM Todays_Deals
						WHERE deal_id = @deal_id
				END
		END

--11
CREATE PROC createGiftCard
@code varchar(10),
@expiry_date datetime,
@amount int,
@admin_username varchar(20)
AS
IF EXISTS(
	SELECT g.code FROM Giftcard g WHERE g.code=@code
)
BEGIN
	UPDATE Giftcard
	SET username=@admin_username, amount=@amount,expiry_date=@expiry_date
	WHERE code=@code
END
ELSE
BEGIN
	INSERT INTO Giftcard VALUES(@code,@expiry_date,@amount,@admin_username)
END

--12
CREATE PROC removeExpiredGiftCard
@code VARCHAR(10)

AS
DECLARE @expiry_date DATETIME
DECLARE @customer_username VARCHAR(20)

SELECT  @expiry_date = expiry_date
FROM Giftcard
WHERE code = @code

IF (Current_timestamp > @expiry_date)
BEGIN
    IF EXISTS(SELECT * FROM Admin_Customer_Giftcard g WHERE g.code = @code)
    BEGIN

        SELECT @customer_username = g.customer_name
        FROM Admin_Customer_Giftcard g
        WHERE g.code = @code

        UPDATE c
        SET c.points = 0
        FROM Customer c
        WHERE c.username = @customer_username

        DELETE
        FROM Admin_Customer_Giftcard
        WHERE code = @code

       
    END
	 DELETE
     FROM Giftcard
     WHERE code = @code
END

--13

CREATE PROC checkGiftCardOnCustomer
@code VARCHAR(10),
@activeGiftCard BIT OUTPUT

AS
DECLARE @count INT

SELECT  @count = COUNT(code)
FROM Admin_Customer_Giftcard
WHERE code = @code

IF @count <> 0
BEGIN
 SET @activeGiftCard = '1'
END

ELSE
BEGIN
	SET @activeGiftCard = '0'
END

PRINT @activeGiftCard

--14
CREATE PROC giveGiftCardtoCustomer
@code VARCHAR(10), 
@customer_username VARCHAR(20),
@admin_username VARCHAR(20)

AS

DECLARE @points INT
DECLARE @check INT
SELECT @check=count(1) FROM Giftcard g WHERE g.code = @code AND g.expiry_date>CURRENT_TIMESTAMP
IF @check <>0
BEGIN

    SELECT @points = amount
    FROM Giftcard
    WHERE code = @code

    INSERT INTO Admin_Customer_Giftcard VALUES (@code, @customer_username, @admin_username, @points)

    UPDATE c
    SET c.points = @points
    FROM Customer c
    WHERE c.username = @customer_username
END

----------------------------------------------------DELIVERY PERSON

--1
CREATE PROC acceptAdminInvitation
@delivery_username VARCHAR(20)

AS
UPDATE Delivery_Person
SET is_activated = '1' 
WHERE  username = @delivery_username

--2
CREATE PROC deliveryPersonUpdateInfo 
@username VARCHAR(20),
@ﬁrst_name VARCHAR(20),
@last_name VARCHAR(20),
@password VARCHAR(20),
@email VARCHAR(50) 

AS
UPDATE Users
SET first_name = @ﬁrst_name,last_name=@last_name,password = @password,email= @email
WHERE username=@username

--3
CREATE PROC viewmyorders
@deliveryperson VARCHAR(20)

AS

SELECT o.*
FROM Orders o INNER JOIN Admin_Delivery_Order a ON o.order_no = a.order_no
WHERE delivery_username = @deliveryperson

--4
CREATE PROC specifyDeliveryWindow
@delivery_username varchar(20),
@order_no int,
@delivery_window varchar(50)

AS

UPDATE Admin_Delivery_Order
SET delivery_window = 'Today between 10 am and 3 pm'
WHERE delivery_username = @delivery_username AND order_no = @order_no
--5
CREATE PROC updateOrderStatusOutforDelivery
@order_no INT

AS

IF EXISTS(SELECT * FROM Admin_Delivery_Order WHERE order_no = @order_no)
BEGIN
    UPDATE Orders
    SET order_status = 'Out for delivery'
    WHERE order_no = @order_no
END

--6
CREATE PROC updateOrderStatusDelivered
@order_no INT

AS
UPDATE Orders
SET order_status = 'Delivered'
WHERE order_no = @order_no
------------------------VENDORS
--1

CREATE PROC postProduct
@vendorname VARCHAR(20),
@productname VARCHAR(20),
@category VARCHAR(20),
@description VARCHAR(20),
@price DECIMAL(10,2),
@color VARCHAR(20)

AS

BEGIN 
INSERT INTO product (product_name,category, product_description,price,final_price,color,vendor_username)
VALUES (@productname , @category, @description,@price,@price,@color,@vendorname)
END
--2


CREATE PROC editProduct
@vendorName VARCHAR(20),
@serialNo INT,
@productName VARCHAR(20),
@category VARCHAR(20),
@productDescription TEXT,
@price DECIMAL(10,2),
@color VARCHAR(20)

AS
BEGIN 
UPDATE product
SET product_name=@productName , category=@category , product_description=@productDescription,
price=@price, color=@color, vendor_username=@vendorName
WHERE serial_no=@serialNo
END
---3

CREATE PROC vendorViewProducts 
@vendorName VARCHAR(20)
AS
BEGIN
SELECT *
FROM product
WHERE vendor_username=@vendorName

END
----4

CREATE PROC viewQuestions
@vendorName VARCHAR(20)

AS
BEGIN

SELECT  c.serial_no,c.customer_name , c.question, c.answer 
FROM customer_question_product c
INNER JOIN product p ON p.vendor_username=@vendorName AND p.serial_no = c.serial_no
END
-----5

CREATE PROC answerQuestions 
@vendorName VARCHAR(20),
@serialNo INT,
@customerName VARCHAR(20),
@answer TEXT

AS
UPDATE customer_question_product
SET answer=@answer
WHERE serial_no=@serialNo AND customer_name=@customerName
------6
CREATE PROC addOffer
@offerAmount INT,
@expiry_date DATETIME

AS
BEGIN
INSERT INTO offer (offer_amount,expiry_date)
VALUES (@offeramount ,@expiry_date)
END 
---------7

CREATE PROC checkOfferOnProduct
@serialNo INT,
@activeOffer BIT OUTPUT

AS
BEGIN
IF EXISTS (SELECT offer_id FROM offersOnProduct WHERE serial_no=@serialNo)
SET @activeOffer='1'
ELSE
SET @activeOffer = '0'

PRINT @activeOffer
END
---8

CREATE PROC applyOffer
@vendorName VARCHAR(20),
@offerid INT,
@serial_no INT


AS

DECLARE @offer_amount INT
DECLARE @check int

SELECT @check=count(1) 
FROM offer WHERE offer_id=@offerid AND expiry_date>CURRENT_TIMESTAMP
IF @check <>0
BEGIN
	
INSERT INTO offersOnProduct VALUES (@offerid,@serial_no)
SELECT @offer_amount = o.offer_amount
FROM offer o
WHERE o.offer_id = @offerid

	
UPDATE p
SET p.final_price =(p.price -(p.price * @offer_amount/100) ) 
FROM Product p
WHERE p.serial_no = @serial_no


END
ELSE

PRINT 'Sorry, offer expired'

----9

CREATE PROC checkandremoveExpiredOffer
@offerId INT

AS 
DECLARE @expired DATETIME

SELECT @expired = expiry_date 
FROM offer
WHERE offer_id=@offerId 

IF(CURRENT_TIMESTAMP > @expired)
BEGIN
	IF NOT EXISTS(SELECT * FROM offersOnProduct WHERE offer_id = @offerId)
		BEGIN
			DELETE FROM offer WHERE offer_id=@offerId
		END
	ELSE
		BEGIN
			UPDATE p

			SET p.final_price = p.price

			FROM Product p INNER JOIN offersOnProduct op ON p.serial_no = op.serial_no
			WHERE op.offer_id = @offerId
			DELETE FROM offer WHERE offer_id = @offerId
		END
END

--10

CREATE PROC deleteProduct
@vendorName VARCHAR(20),
@serialNo INT

AS
BEGIN
DELETE FROM product WHERE serial_no=@serialNo AND vendor_username=@vendorName
END
--11
CREATE PROC orderExists
@customer VARCHAR(20),
@orderID INT,
@exist BIT OUTPUT
AS

IF EXISTS( SELECT * FROM Orders o WHERE o.order_no = @orderID )
BEGIN
SET @exist = '1';
END

ELSE
SET @exist = '0';
