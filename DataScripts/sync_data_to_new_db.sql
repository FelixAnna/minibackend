/* 
DELETE FROM [dbo].[AlipayUsers]
DELETE FROM [dbo].[OrderItemOptions]
DELETE FROM [dbo].[OrderItems]
DELETE FROM [dbo].[Orders]

--sync users
select 'INSERT INTO [dbo].[AlipayUsers](Id, [AlibabaUserId],[AlipayUserId], [AlipayName],[AlipayPhoto],[CreatedAt]) 
VALUES'
union all
SELECT concat('(''',Id, ''',''', [AlibabaUserId], ''',''',[AlipayUserId], ''',N''',
  case when [AlipayName] = 'undefined' then '' else AlipayName end 
, ''',''',
case when [AlipayPhoto] = 'undefined' then '' else AlipayPhoto end 
, ''',''',[CreatedAt], '''),')
FROM [dbo].[AlipayUsers]

union all
select 'set IDENTITY_INSERT [dbo].[Orders] on'
union all

--sync orders
select 'INSERT INTO [dbo].[Orders] (OrderId, Options, State, CreatedAt, CreatedBy) VALUES'
union all
select --distinct orderId, 
concat('(''',OrderId,''','
,N'N''[{"Id":1,"Name":"加饭","Type":"bool","Default":"false","Order":"1"},{"Id":2,"Name":"加辣","Type":"bool","Default":"false","Order":"2"}]'','
, State,',''', CreatedAt,''',''', CreatedBy,'''),')
from
(select ShopId%197*-1 as OrderId, State, CreatedAt, CreatedBy
from [dbo].[Orders]
) t
--order by OrderId

union all
select 'set IDENTITY_INSERT [dbo].[Orders] off'
union all
select 'set IDENTITY_INSERT [dbo].[OrderItems] on'
union all

--sync order items
select 'insert into [dbo].[OrderItems] (OrderItemId, Name, Price, Remark, CreatedAt, CreatedBy, OrderId)
VALUES'
union all
SELECT concat('(', oi.OrderItemId,',N''', Name,''',', Price,',N''',Remark,''',''',oi.CreatedAt,''',''',oi.CreatedBy,''',', ShopId%197*-1 ,'),')
FROM [dbo].[OrderItems] oi 
inner join [dbo].[Orders] o on oi.OrderId = o.OrderId

union all
select 'set IDENTITY_INSERT [dbo].[OrderItems] off'
union all
select 'set IDENTITY_INSERT [dbo].[OrderItemOptions] on'
union all

--sync order item options
select 'insert into [dbo].[OrderItemOptions] (Id, Name, Value, OrderItemId)
VALUES'
union all
SELECT concat('(', Id,',N''', Name,''',N''',case when [Value] =1 then 'true' else 'false' end,''',',OrderItemId,'),')
FROM [dbo].[OrderItemOptions]

union all

select 'set IDENTITY_INSERT [dbo].[OrderItemOptions] off'
*/