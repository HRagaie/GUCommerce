CREATE TABLE Users 
( username varchar(20) PRIMARY KEY NOT NULL,
password VARCHAR(20),
first_name VARCHAR(20),
last_name VARCHAR(20),
email VARCHAR(50) UNIQUE NOT NULL
);

CREATE TABLE user_mobile_number
(mobile_number VARCHAR(20) NOT NULL,
username VARCHAR(20),
FOREIGN KEY(username) REFERENCES Users,
PRIMARY KEY(mobile_number, username)
);

CREATE TABLE User_addresses
(
	address VARCHAR(100), 
    username VARCHAR(20) FOREIGN KEY REFERENCES Users, 
	PRIMARY KEY(address,username)
)

CREATE TABLE Customer 
( points INT DEFAULT 0,
username VARCHAR(20) PRIMARY KEY FOREIGN KEY REFERENCES Users 
);

CREATE TABLE Admins 
(
username VARCHAR(20) PRIMARY KEY FOREIGN KEY REFERENCES Users  
);

CREATE TABLE Vendor 
(activated BIT NOT NULL DEFAULT '0',
company_name VARCHAR(20) NOT NULL,
bank_acc_no VARCHAR(20),
admin_username VARCHAR(20) FOREIGN KEY REFERENCES Admins,                                       
username VARCHAR(20) PRIMARY KEY FOREIGN KEY REFERENCES Users 
);

CREATE TABLE Delivery_Person 
(is_activated BIT NOT NULL,
username VARCHAR(20) PRIMARY KEY FOREIGN KEY REFERENCES Users 
);

CREATE TABLE Credit_Card 
(number VARCHAR(20) PRIMARY KEY,
expiry_date DATE NOT NULL,
cvv_code VARCHAR(4) NOT NULL
);


CREATE TABLE Delivery 
(id INT PRIMARY KEY IDENTITY,
type VARCHAR(20) NOT NULL,
time_duration INT NOT NULL,
fees decimal(5,3) NOT NULL,
username VARCHAR(20) FOREIGN KEY REFERENCES Admins
);

CREATE TABLE Giftcard
(
code VARCHAR(10) PRIMARY KEY,
expiry_date DATETIME,
amount INT,
username VARCHAR(20) FOREIGN KEY REFERENCES Admins
);

CREATE TABLE Orders
(
order_no INT PRIMARY KEY,
order_date DATE,
total_amount decimal(10,2) ,
cash_amount decimal(10,2) DEFAULT 0,                                          
credit_amount decimal(10,2) DEFAULT 0,
payment_type VARCHAR(20) NOT NULL,
order_status VARCHAR(25) NOT NULL,
remaining_days INT,
time_limit INT,
Gift_Card_code_used VARCHAR(10) FOREIGN KEY REFERENCES Giftcard ON DELETE SET NULL ON UPDATE CASCADE,
customer_name VARCHAR(20) FOREIGN KEY REFERENCES Customer,
delivery_id INT FOREIGN KEY REFERENCES Delivery ON DELETE SET NULL ON UPDATE CASCADE,
creditCard_number VARCHAR(20) FOREIGN KEY REFERENCES Credit_Card ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE Product
(
serial_no INT PRIMARY KEY IDENTITY,
product_name VARCHAR(20) NOT NULL,
category VARCHAR(20),
product_description VARCHAR(250),
price DECIMAL(10,2),
final_price DECIMAL(10,2),
color VARCHAR(20),
available BIT DEFAULT '1',
rate INT,
vendor_username VARCHAR(20) FOREIGN KEY REFERENCES Vendor,
customer_username VARCHAR(20) FOREIGN KEY REFERENCES Customer, 
customer_order_id INT FOREIGN KEY REFERENCES Orders ON DELETE SET NULL ON UPDATE CASCADE
);

CREATE TABLE CutomerAddstoCartProduct
(
serial_no INT FOREIGN KEY REFERENCES Product ON DELETE CASCADE ON UPDATE CASCADE,                                     
customer_name VARCHAR(20) FOREIGN KEY REFERENCES Customer,
PRIMARY KEY(serial_no, customer_name)
);

CREATE TABLE Todays_Deals
(
deal_id INT IDENTITY PRIMARY KEY,
deal_amount INT ,
expiry_date DATETIME,
admin_username VARCHAR(20) FOREIGN KEY REFERENCES Admins
);

CREATE TABLE Todays_Deals_Product
(
deal_id INT FOREIGN KEY REFERENCES Todays_deals ON DELETE CASCADE ON UPDATE CASCADE,
serial_no INT FOREIGN KEY REFERENCES Product ON DELETE CASCADE ON UPDATE CASCADE,
PRIMARY KEY( deal_id, serial_no)
);

CREATE TABLE offer
(
offer_id INT IDENTITY PRIMARY KEY,
offer_amount INT,
expiry_date DATETIME
);

CREATE TABLE offersOnProduct
(
offer_id INT FOREIGN KEY REFERENCES offer ON DELETE CASCADE ON UPDATE CASCADE,
serial_no INT FOREIGN KEY REFERENCES Product ON DELETE CASCADE ON UPDATE CASCADE,
PRIMARY KEY (offer_id, serial_no)
);

CREATE TABLE Customer_Question_Product
(
serial_no INT FOREIGN KEY REFERENCES Product ON DELETE CASCADE ON UPDATE CASCADE,
customer_name VARCHAR(20) FOREIGN KEY REFERENCES Customer,
question VARCHAR(50),
answer TEXT,
PRIMARY KEY( serial_no, customer_name)
);

CREATE TABLE Wishlist
(
username VARCHAR(20) FOREIGN KEY REFERENCES Customer,
name VARCHAR(20),
PRIMARY KEY( username, name)
);


CREATE TABLE Wishlist_Product
(
username VARCHAR(20),
wish_name VARCHAR(20),
serial_no INT FOREIGN KEY REFERENCES Product ON DELETE CASCADE ON UPDATE CASCADE,
PRIMARY KEY(username,wish_name,serial_no),
FOREIGN KEY(username,wish_name) REFERENCES Wishlist ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE Admin_Customer_Giftcard
(
code VARCHAR(10) FOREIGN KEY REFERENCES Giftcard ON DELETE CASCADE ON UPDATE CASCADE,
customer_name VARCHAR(20) FOREIGN KEY REFERENCES Customer,
admin_username VARCHAR(20) FOREIGN KEY REFERENCES Admins,                                 
remaining_points INT,
PRIMARY KEY(code, customer_name, admin_username)
);

CREATE TABLE Admin_Delivery_Order
(
delivery_username VARCHAR(20) FOREIGN KEY REFERENCES Delivery_person,                    
order_no INT FOREIGN KEY REFERENCES Orders ON DELETE CASCADE ON UPDATE CASCADE,
admin_username VARCHAR(20) FOREIGN KEY REFERENCES Admins,                              
delivery_window VARCHAR(50),
PRIMARY KEY( delivery_username, order_no)
);

CREATE TABLE Customer_CreditCard
(
customer_name VARCHAR(20) FOREIGN KEY REFERENCES Customer,
cc_number VARCHAR(20) FOREIGN KEY REFERENCES Credit_Card ON DELETE CASCADE ON UPDATE CASCADE,
PRIMARY KEY( customer_name, cc_number)
);