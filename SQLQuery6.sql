
SELECT  SUM(Marks) FROM StudentData GROUP BY Name

select TOP 1 StudentData.Name, StudentData.Id ,SUM(Marks) From StudentData GROUP BY ID,Name