-- ============================================================
-- SmartClinic v2 Migration Script
-- Run this in SQL Server Management Studio against SmartClinicDB
-- ============================================================

USE SmartClinicDB;
GO

-- ── 1. Patient Profile Fields on AspNetUsers ──────────────────
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'BloodGroup')
    ALTER TABLE AspNetUsers ADD BloodGroup NVARCHAR(5) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'Address')
    ALTER TABLE AspNetUsers ADD Address NVARCHAR(300) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'DateOfBirth')
    ALTER TABLE AspNetUsers ADD DateOfBirth DATETIME2 NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'Gender')
    ALTER TABLE AspNetUsers ADD Gender NVARCHAR(10) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'ProfileImage')
    ALTER TABLE AspNetUsers ADD ProfileImage NVARCHAR(255) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'EmergencyContact')
    ALTER TABLE AspNetUsers ADD EmergencyContact NVARCHAR(50) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('AspNetUsers') AND name = 'EmergencyPhone')
    ALTER TABLE AspNetUsers ADD EmergencyPhone NVARCHAR(15) NULL;

PRINT 'Patient profile columns added to AspNetUsers';

-- ── 2. Doctor Schedule Fields on Doctors ──────────────────────
IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'Experience')
    ALTER TABLE Doctors ADD Experience NVARCHAR(200) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'Languages')
    ALTER TABLE Doctors ADD Languages NVARCHAR(300) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'HospitalAffiliation')
    ALTER TABLE Doctors ADD HospitalAffiliation NVARCHAR(200) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'LicenseNumber')
    ALTER TABLE Doctors ADD LicenseNumber NVARCHAR(20) NULL;

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'EmploymentType')
    ALTER TABLE Doctors ADD EmploymentType NVARCHAR(20) NOT NULL DEFAULT 'FullTime';

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'WorkingDays')
    ALTER TABLE Doctors ADD WorkingDays NVARCHAR(50) NULL DEFAULT 'Mon,Tue,Wed,Thu,Fri';

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'ShiftStart')
    ALTER TABLE Doctors ADD ShiftStart NVARCHAR(5) NULL DEFAULT '09:00';

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'ShiftEnd')
    ALTER TABLE Doctors ADD ShiftEnd NVARCHAR(5) NULL DEFAULT '17:00';

IF NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('Doctors') AND name = 'SlotDurationMinutes')
    ALTER TABLE Doctors ADD SlotDurationMinutes INT NOT NULL DEFAULT 30;

PRINT 'Doctor schedule columns added to Doctors';

-- ── 3. Backfill default schedule for existing doctors ─────────
UPDATE Doctors
SET
    EmploymentType = ISNULL(EmploymentType, 'FullTime'),
    WorkingDays    = ISNULL(WorkingDays,    'Mon,Tue,Wed,Thu,Fri'),
    ShiftStart     = ISNULL(ShiftStart,     '09:00'),
    ShiftEnd       = ISNULL(ShiftEnd,       '17:00'),
    SlotDurationMinutes = ISNULL(SlotDurationMinutes, 30)
WHERE IsDeleted = 0;

PRINT 'Existing doctors backfilled with default schedule';

-- ── 4. Verify ─────────────────────────────────────────────────
SELECT
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME IN ('AspNetUsers','Doctors')
  AND COLUMN_NAME IN (
    'BloodGroup','Address','DateOfBirth','Gender','ProfileImage',
    'EmergencyContact','EmergencyPhone',
    'Experience','Languages','HospitalAffiliation','LicenseNumber',
    'EmploymentType','WorkingDays','ShiftStart','ShiftEnd','SlotDurationMinutes'
  )
ORDER BY TABLE_NAME, COLUMN_NAME;

PRINT 'Migration complete.';
GO
