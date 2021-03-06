USE [EmployeesMSDb]
GO
/****** Object:  StoredProcedure [dbo].[SP_DayEvent]    Script Date: 09.01.2021 02:35:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[SP_DayEvent]

	@DayEventId varchar(50)

	,@Note varchar(MAX)	

	,@EventDate datetime

	,@OperationType int


AS

BEGIN TRAN

	

	IF(@OperationType = 1) --Insert

	BEGIN

		

		SET @DayEventId = (SELECT COUNT(*) FROM DayEvent) + 1


		INSERT INTO DayEvent  (DayEventId,	[Note],		EventDate)

			           VALUES(@DayEventId,	@Note,		@EventDate)


		SELECT * FROM DayEvent WHERE DayEventId=@DayEventId


	END

	ELSE IF(@OperationType = 2) --Update

	BEGIN

		IF (@DayEventId = 0)

		BEGIN

			ROLLBACK

				RAISERROR (N'Invalid DayEvent !!!~',16,1);	

			RETURN

		END


		UPDATE DayEvent SET [Note]=@Note

						   ,EventDate=@EventDate

						WHERE DayEventId=@DayEventId


		SELECT * FROM DayEvent WHERE DayEventId=@DayEventId

	END

	ELSE IF(@OperationType = 3) --Delete

	BEGIN

		IF (@DayEventId = 0)

		BEGIN

			ROLLBACK

				RAISERROR (N'Invalid DayEvent !!!~',16,1);	

			RETURN

		END


		DELETE FROM DayEvent WHERE DayEventId=@DayEventId

	END

COMMIT TRAN
GO
