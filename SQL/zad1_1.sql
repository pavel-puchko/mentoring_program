SELECT OrderID, ShippedDate, ShipVia
 FROM Orders
 WHERE ShipVia >= 2 AND ShippedDate >= CONVERT(datetime, '1998-05-06');

SELECT OrderID, CASE WHEN ShippedDate IS NULL THEN 'Not Shipped' ELSE CONVERT(nvarchar(23), ShippedDate, 121) END
 FROM Orders;

SELECT OrderID AS [Order Number], CASE WHEN ShippedDate IS NULL THEN 'Not Shipped' ELSE CONVERT(nvarchar(23), ShippedDate, 121) END AS [Shipped Date]
 FROM Orders
 WHERE ShippedDate > CONVERT(datetime, '1998-05-06') OR ShippedDate IS NULL;
