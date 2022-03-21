UPDATE CUSTOMER SET Middle_Name = NULL WHERE Middle_Name = '';
UPDATE PRODUCT SET Product_Description =NULL WHERE Product_Description = '';
UPDATE ORDERLINE SET Amount = Selling_Price * Quantity_ordered;

UPDATE [ORDER] SET Order_Amount =(SELECT SUM(AMOUNT) FROM ORDERLINE GROUP BY Order_ID ORDER BY SUM(Amount) OFFSET 0 ROWS);