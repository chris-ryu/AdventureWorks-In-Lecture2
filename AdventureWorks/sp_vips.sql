USE AdventureWorks
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetVIPs]
        @averageTotalDue decimal,
        @limit smallint
        AS
        BEGIN
        SET NOCOUNT ON;
SELECT TOP(@limit) [c].[CustomerID] AS [CustomerId], ([c].[FirstName] + N' ') + [c].[LastName] AS [CustomerName], (
    SELECT AVG([s1].[TotalDue])
    FROM [SalesLT].[SalesOrderHeader] AS [s1]
    WHERE [c].[CustomerID] = [s1].[CustomerID]) AS [AverageTotalDue]
FROM [SalesLT].[Customer] AS [c]
WHERE EXISTS (
    SELECT 1
    FROM [SalesLT].[SalesOrderHeader] AS [s]
    WHERE [c].[CustomerID] = [s].[CustomerID]
    GROUP BY [s].[CustomerID]
    HAVING AVG([s].[TotalDue]) > @averageTotalDue)
ORDER BY (
    SELECT AVG([s0].[TotalDue])
    FROM [SalesLT].[SalesOrderHeader] AS [s0]
    WHERE [c].[CustomerID] = [s0].[CustomerID]) DESC
    END
    GO