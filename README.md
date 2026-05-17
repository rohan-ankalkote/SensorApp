# Issues found

- Variable/Column names are not readable
- Exceptions are not handled properly, connection is not closed in finally and empty catch blocks that are suppressing errors
- Database tablesa are not normalized
  
  - t_dat table is used to store device readings and threshold alerts both identified by typ columns.
  - t_dat has st column which is not required since t_dev already has that status.

  ## Issues while saving devices

  - No device id is mentioned in SaveDev while inserting
  - ref in log table is always having 0, it should be device id
  - device table stores non-atomic values in cfg column
  - try-catch returns false instead of handling exception

  ## Issues while saving device readings

  - Device tables datetime column in updated despite no changes done to that table
  - while parsing threshold, empty catch block
  - Type and Status seems to be related to device but duplicated
  - While inserting threshold alert, device related infor is duplicated
  - catch block is returning false, and not handling error
  - connection should be closed in finally

  ## GetAll

  - Improper exception handling, multiple nested try-catch blocks.
  - Since threshold alerts also saved in same table, duplicate entries possible
  - Method is GetAll but query limits to 1000

  ## Get Devices

  - Device status can be string to be more redable instead of int.
  - V and V2 used to store threshold and interval, not redable.
  - Empty catch block

  ## Calc

  - Device Type is hardcoded and should be used by joining Device table instead of new column in Reading table.
  - Cutoff time can be provided through parameter instead of hardcoding
  - If max value is greater than threshold, then threshold alert should be in another table.
  - Improper exception handling.

  ## Get Logs

  - Since deviceId is matched with ref, ref should be renamed as DeviceId
  - Improper exception handling.

## Fixes that can be done

- Threshold and its related values should not be stored in cfg column, instead separate columns
- There can be many threshold alerts for a single reading, so one - many relationship. Create separate table for it.
- To store threshold alerts we should not duplicate reading information, this will cause incorrect calculations.
- Handle exception in one common place.
- Status and Device Type can be stored as string to make them more redable.

## Technologies and Architecture

- Instead of using System.Data.Sqlite, we can use EntityFrameworkCore.Sqlite to avoid writing queries for specific DB
- Readings are stored after every intervals which makes them suitable for storing in time series based databases.
- I choose clean architecture to separate to domain logic with persistence logic.
- Since appliation layer is not dependent on persistence logic, it can be independently tested.
- Added new feature will not required modification in any other existing code, thus it can be scaled.