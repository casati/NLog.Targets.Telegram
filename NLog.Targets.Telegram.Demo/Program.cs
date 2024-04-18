var logger = LogManager.GetCurrentClassLogger();

logger.Debug("Program has started");

try
{
    throw new InvalidOperationException("Operation not allowed");
}
catch(Exception ex)
{
    logger.Error(ex, "An error has occurred");
}
