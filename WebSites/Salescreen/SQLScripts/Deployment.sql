--Migrate zip_prefix table WITHOUT data
--Migrate warehouse_zip_prefix_leadtime table WITHOUT data
--Migrate warehouse_type table WITH data
--Add field Form.warehouse_type_id and create FK to warehouse_type table

--dev dont run in prod
--update form
--set warehouse_type_id = 1
--where form_id in (66,69,71,72,73,74,78,79,80)

--prod
update form
set warehouse_type_id = 1
where form_id in (44,47,48,49,50,56)


BEGIN TRAN WarehouseLeadTime

DECLARE @ident int

INSERT INTO zip_prefix VALUES (
5,
'US-NY',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
6,
'US-PR',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
7,
'US-PR',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
8,
'US-VI',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
9,
'US-PR',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
10,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
11,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
12,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
13,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
14,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
15,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
16,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
17,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
18,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
19,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
20,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
21,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
22,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO zip_prefix VALUES (
23,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
24,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
25,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
26,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
27,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
28,
'US-RI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
29,
'US-RI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
30,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
31,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
32,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
33,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
34,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
35,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
36,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
37,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
38,
'US-NH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
39,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
40,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
41,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
42,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
43,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
44,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
45,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
46,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)



INSERT INTO zip_prefix VALUES (
47,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
48,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
49,
'US-ME',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
50,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
51,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
52,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
53,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
54,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
55,
'US-MA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
56,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
57,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
58,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
59,
'US-VT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
60,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
61,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
62,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
63,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
64,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
65,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
66,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
67,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
68,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
69,
'US-CT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
70,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
71,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
72,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
73,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
74,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
75,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
76,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
77,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
78,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
79,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
80,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
81,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
82,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
83,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
84,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
85,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
86,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
87,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
88,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
89,
'US-NJ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
90,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
91,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
92,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
93,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
94,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
95,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
96,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
97,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
98,
'US-AE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
100,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
101,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
102,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
103,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
104,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
105,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
106,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
107,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
108,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
109,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
110,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
111,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
112,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
113,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
114,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
115,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
116,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
117,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
118,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
119,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
62,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
120,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
121,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
122,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
123,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
124,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
125,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
126,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
127,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
128,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
129,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
130,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
131,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
132,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
133,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
134,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
135,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
136,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
137,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
138,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
139,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
140,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
141,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
142,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
143,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
144,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
145,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
146,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
147,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
148,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
149,
'US-NY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
150,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
151,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
152,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
153,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
154,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
155,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
156,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
157,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
158,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
159,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
160,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
161,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
162,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
163,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
164,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
165,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
166,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
167,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
168,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
169,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
170,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
171,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
172,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
173,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
174,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
175,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
176,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
177,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
178,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
179,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
180,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
181,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
182,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
183,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
184,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
185,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
186,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
187,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
188,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
189,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
190,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
191,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
192,
'US-PA',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
193,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
194,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
195,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
196,
'US-PA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
197,
'US-DE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
198,
'US-DE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
199,
'US-DE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
200,
'US-DC',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
201,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
202,
'US-DC',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
203,
'US-DC',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
204,
'US-DC',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
205,
'US-DE',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
206,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
207,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
208,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
209,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
210,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
211,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
212,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
214,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
215,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
216,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
217,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
218,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
219,
'US-MD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
21,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
220,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
221,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
222,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
223,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
224,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
225,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
226,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
227,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
228,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
229,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
230,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
231,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
232,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
233,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
234,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
235,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
236,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
237,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
238,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
239,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
240,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
241,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
242,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
243,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
244,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
245,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
246,
'US-VA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
247,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
248,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
249,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
250,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
251,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
252,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
253,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
254,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
255,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
256,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
257,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
258,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
259,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
260,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
261,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
262,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
263,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
264,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
265,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
266,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
267,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
268,
'US-WV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
270,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
271,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
272,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
273,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
274,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
275,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
276,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
277,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
278,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
279,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
280,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
281,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
282,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
283,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
284,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
285,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
286,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
287,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
288,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
289,
'US-NC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
290,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
291,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
292,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
293,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
294,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
295,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
296,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
297,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
298,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
299,
'US-SC',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
300,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
301,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
302,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
303,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
304,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
305,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
306,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
307,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
308,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
309,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
310,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
311,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
312,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
313,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
314,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
315,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
316,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
317,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
318,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
319,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
320,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
321,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
322,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
323,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
324,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
325,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
326,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
327,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
328,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
329,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
330,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
331,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
332,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
333,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
334,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
335,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
336,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
337,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
338,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
339,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
340,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
341,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
342,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
343,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
--



INSERT INTO zip_prefix VALUES (
344,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
345,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
346,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
347,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
348,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
--

INSERT INTO zip_prefix VALUES (
349,
'US-FL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
81,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
350,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
351,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
352,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
354,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
355,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
356,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
357,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
358,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
359,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
360,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
361,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
362,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
363,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
364,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
365,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
366,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
367,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
368,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
369,
'US-AL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
370,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
371,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
372,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
373,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
374,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
375,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
376,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
377,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
378,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
379,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
380,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
381,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
382,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
383,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
384,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
385,
'US-TN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
386,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
387,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
388,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
389,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
390,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
391,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
392,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
393,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
394,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
395,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
396,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
397,
'US-MS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
398,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
399,
'US-GA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
24,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
400,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
401,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
402,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
403,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
404,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
405,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
406,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
407,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
408,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
409,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
410,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
411,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
412,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
413,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
414,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
415,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
416,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
417,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
418,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
420,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
421,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
422,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
423,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
424,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
425,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
426,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
427,
'US-KY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
430,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
431,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
432,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
433,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
434,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
435,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
436,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
437,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
438,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
439,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
440,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
441,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
442,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
443,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
444,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
445,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
446,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
447,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
448,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
449,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
450,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
451,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
452,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
453,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
454,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
455,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
456,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
457,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
458,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
459,
'US-OH',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
60,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
460,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
461,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
462,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
463,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
464,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
465,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
466,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
467,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
468,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
469,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
470,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
471,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
472,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
473,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
474,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
475,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
476,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
477,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
478,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
479,
'US-IN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
480,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
481,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
482,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
483,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
484,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
485,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
486,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
487,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
488,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
489,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
490,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
491,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
492,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
493,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
494,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
495,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
496,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
497,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49701,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49701,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49705,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49705,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49706,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49706,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49707,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49707,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49709,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49709,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49710,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49710,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49711,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49711,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49712,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49712,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49713,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49713,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49715,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49715,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49716,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49716,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49717,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49717,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49718,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49718,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49719,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49719,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49720,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49720,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49721,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49721,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49722,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49722,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49723,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49723,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49724,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49724,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49725,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49725,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49726,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49726,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49727,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49727,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49728,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49728,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49729,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49729,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49730,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49730,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49733,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49733,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49734,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49734,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49735,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49735,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49736,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49736,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49737,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49737,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49738,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49738,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49739,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49739,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49740,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49740,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49743,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49743,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49744,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49744,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49745,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49745,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49746,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49746,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49747,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49747,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49748,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49748,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49749,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49749,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49751,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49751,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49752,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49752,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49753,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49753,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49755,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49755,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49756,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49756,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49757,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49757,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49759,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49759,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49760,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49760,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49761,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49761,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49762,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49762,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49764,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49764,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49765,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49765,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49766,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49766,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49768,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49768,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49769,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49769,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49770,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49770,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49774,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49774,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49775,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49775,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49776,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49776,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49777,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49777,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49779,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49779,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49780,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49780,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49781,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49781,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49782,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49782,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49783,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49783,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49784,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49784,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49785,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49785,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49788,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49788,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49790,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49790,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49791,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49791,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49792,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49792,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49793,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
49793,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49795,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49795,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49796,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49796,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49797,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49797,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49799,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
49799,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)













































INSERT INTO zip_prefix VALUES (
498,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
499,
'US-MI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
500,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
501,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
502,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
503,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
504,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
505,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
506,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
507,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
508,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
509,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
510,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
511,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
512,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
513,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
514,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
515,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
516,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
520,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
521,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
522,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
523,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
524,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
525,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
526,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
527,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
528,
'US-IA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
530,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
531,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
532,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
534,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
535,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
537,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
538,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
539,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
540,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
541,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
542,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
543,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
544,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
545,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
546,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
547,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
548,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
549,
'US-WI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
550,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
551,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
553,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
554,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
555,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
556,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
557,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
558,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
559,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
560,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
561,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
562,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
563,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
564,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
565,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
566,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
567,
'US-MN',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
569,
'US-DC',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
570,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
571,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
572,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
573,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
574,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
575,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
576,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
577,
'US-SD',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
580,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
581,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
582,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
583,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
584,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
585,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
586,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
587,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
588,
'US-ND',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
12,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
590,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
591,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
592,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
593,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
594,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
595,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
596,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
597,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
598,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
599,
'US-MT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
600,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
601,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
602,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
603,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
604,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
605,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
606,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
607,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
608,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
609,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
610,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
611,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
612,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
613,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
614,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
615,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
616,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
617,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
618,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
619,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
40,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
620,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
622,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
623,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
624,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
625,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
626,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
627,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
628,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
629,
'US-IL',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
630,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
631,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
633,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
634,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
635,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
636,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
637,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
638,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
639,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
640,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
641,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
644,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
645,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
646,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
647,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
648,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
649,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
650,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
651,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
652,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
653,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
654,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
655,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
656,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
657,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
658,
'US-MO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
660,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
661,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
662,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
664,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
665,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
666,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
667,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
668,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
669,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
670,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
671,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
672,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
673,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
674,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
675,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
676,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
677,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
678,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
679,
'US-KS',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
680,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
681,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
683,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
684,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
685,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
686,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
687,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
688,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
689,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
690,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
691,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
692,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
693,
'US-NE',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
700,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
701,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
703,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
704,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
705,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
706,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
707,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
708,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
710,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
711,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
712,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
713,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
714,
'US-LA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
716,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
717,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
718,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
719,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
720,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
721,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
722,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
723,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
724,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
725,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
726,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
727,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
728,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,--
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
729,
'US-AR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
730,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
731,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
733,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
----


INSERT INTO zip_prefix VALUES (
734,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
735,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
736,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
737,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
738,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
739,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
740,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
741,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
743,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
744,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
745,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
746,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
747,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
748,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
749,
'US-OK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
750,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
751,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
752,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
753,
'US-TX',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
754,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
755,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
756,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
757,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
758,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
759,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
760,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
761,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
762,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
763,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
764,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
765,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
766,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
767,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
768,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
769,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
770,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
772,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
773,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
774,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
775,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
776,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
777,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
778,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
779,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
780,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
781,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
782,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
783,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
784,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
785,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
786,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
787,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
788,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
789,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
65,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
790,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
791,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
792,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
793,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
794,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
795,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
796,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
797,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
798,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
799,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
800,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
801,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
802,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
803,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
804,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
805,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
806,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
807,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
808,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
809,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
810,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
811,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
812,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
813,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
814,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
815,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
816,
'US-CO',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
820,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
821,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
822,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
823,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
824,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
825,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
826,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
827,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
828,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
829,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
830,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
831,
'US-WY',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
832,
'US-ID',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
833,
'US-ID',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
834,
'US-ID',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
835,
'US-ID',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
836,
'US-ID',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
837,
'US-ID',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
838,
'US-ID',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
5,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
840,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
841,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
842,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
843,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
844,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
845,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
846,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
847,
'US-UT',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
850,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
852,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
853,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
855,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
856,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
857,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
859,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
860,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
863,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
864,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
865,
'US-AZ',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
870,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
871,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
872,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
873,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
874,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
875,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
877,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
878,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
879,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
880,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
881,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
882,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
883,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
884,
'US-NM',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
4,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
55,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
885,
'US-TX',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
889,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
890,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
891,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
893,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
894,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
895,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
896,
'US-NV',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
897,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
898,
'US-NV',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
3,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
25,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
3,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
900,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
901,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
902,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
903,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
904,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
905,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
906,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
907,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
908,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
910,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
911,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
912,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
913,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
914,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
915,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
916,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
917,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
918,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
919,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
920,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
921,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
922,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
923,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
924,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
925,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
926,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
927,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
928,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
930,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
931,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
932,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
933,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
934,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
935,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
936,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
937,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
938,
'US-CA',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
939,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
75,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
940,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
941,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
942,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
--


INSERT INTO zip_prefix VALUES (
943,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
944,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
945,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
946,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
947,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
948,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
949,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
950,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
951,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
952,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
953,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
954,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
955,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
956,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
957,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
958,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
959,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
960,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
961,
'US-CA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
18,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
962,
'US-AP',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
963,
'US-AP',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
964,
'US-AP',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
965,
'US-AP',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
966,
'US-AP',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
967,
'US-HI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
17,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
17,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO zip_prefix VALUES (
968,
'US-HI',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
17,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
17,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO zip_prefix VALUES (
969,
'US-GU',
getdate(),
101690,
getdate(),
101690
)


INSERT INTO zip_prefix VALUES (
970,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
971,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
972,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
973,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
974,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
975,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
976,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
977,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
978,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
979,
'US-OR',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
980,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
981,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
982,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
983,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
984,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
985,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
986,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
988,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
989,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
990,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
991,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
992,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
993,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
994,
'US-WA',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
2,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
2,
0,
getdate(),
101690,
getdate(),
101690)


INSERT INTO zip_prefix VALUES (
995,
'US-AK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
7,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
7,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO zip_prefix VALUES (
996,
'US-AK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
7,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
7,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO zip_prefix VALUES (
997,
'US-AK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
7,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
7,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO zip_prefix VALUES (
998,
'US-AK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
7,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
7,
0,
getdate(),
101690,
getdate(),
101690)

INSERT INTO zip_prefix VALUES (
999,
'US-AK',
getdate(),
101690,
getdate(),
101690
)
SELECT @ident = SCOPE_IDENTITY()
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-05-01',
'2008-10-01',
7,
0,
getdate(),
101690,
getdate(),
101690)
INSERT INTO warehouse_zip_prefix_leadtime VALUES (
83,
@ident,
NULL,
'2008-10-01',
'2009-05-01',
7,
0,
getdate(),
101690,
getdate(),
101690)

--Commit transaction here

--Switch zip_prefix.zip_prefix to varchar(50)

--Run this to add 0's before
update zip_prefix
set zip_prefix = '00' + zip_prefix
where len(zip_prefix) = 1

update zip_prefix
set zip_prefix = '0' + zip_prefix
where len(zip_prefix) = 2
