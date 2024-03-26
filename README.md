# NLog.Targets.Telegram

An NLog target for Telegram

## Usage

1. Create a [Telegram Bot](https://core.telegram.org/bots#3-how-do-i-create-a-bot).

2. Install [NLog.Targets.Telegram](https://www.nuget.org/packages/NLog.Targets.Telegram/) package from NuGet

3. Configure NLog to use `NLog.Targets.Telegram`:

### NLog.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" autoReload="true">

    <extensions>
        <add assembly="NLog.Targets.Telegram" />
    </extensions>

    <targets>
        <target xsi:type="Telegram" name="myTelegramChannel" 
                layout="*${level:uppercase=true}* @ ${logger}${newline}${message}${newline}```${exception:format=toString,Data:maxInnerExceptionLevel=10}```"
                botToken="xxxxx:xxxxxxxxxxxxxx"
                chatId="xxxxxxx"
                parseMode="MARKDOWN" />
    </targets>

    <rules>
        <logger name="*" minlevel="Debug" writeTo="myTelegramChannel" />
    </rules>

</nlog>
```

### Configuration Options

Key         | Description
----------: | -----------
botToken    | Your Telegram bot token
chatId      | Unique identifier for the message recipient, user or channel ID
parseMode   | Optional. Telegram Message [formatting options](https://core.telegram.org/bots/api#formatting-options): `DEFAULT`, `MARKDOWN` or `HTML`


#### Notes

Setting `parseMode = "HTML"` tags in `layout` must be escaped, i.g. `layout="&lt;b&gt; ${message} &lt;/b&gt;"`
