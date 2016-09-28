IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Region'))

BEGIN
	EXEC SP_RENAME 'Region', 'Regions'
END
