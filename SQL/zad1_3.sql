SELECT OrderID
 FROM [Order Details]
 WHERE Quantity BETWEEN 3 AND 10;

SELECT CustomerID, Country
 FROM Customers
 WHERE LEFT(Country, 1) BETWEEN 'b' AND 'g'
 ORDER BY Country;

SELECT CustomerID, Country
 FROM Customers
 WHERE LEFT(Country, 1) >= 'b' AND LEFT(Country, 1) <= 'g'
 ORDER BY Country;