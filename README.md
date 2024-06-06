# Lock Screen Override Exploit

Back in November 2023, I discovered a way to override the advertisements that display on the Windows lock screen. While Windows users expect the content on their lock screen to be curated by Microsoft, that is not strictly the case any more.

The code in this repository is made to exploit the content delivery system used to curate content on Windows lock screens.

```
NOTE:
I reported this to Microsoft security back in January 2024. They didn't think it was serious, so now I'm releasing my repro code publicly.
```

To get started, download the zip-file from releases and run `lockexp` in your command-line terminal.

Command-line example:

```bash
lockexp --title "All ur base r belong 2 us" --description "Click below 2 redeem ur base" --button "Redeem!" --url "https://www.youtube.com/watch?v=dQw4w9WgXcQ"`
```

You can specify other schemes besides `http` or `https`, such as when the device has custom scheme handlers. A common example would be the  `microsoft-edge` scheme, which is handled by Edge to open URLs in Edge.

If you wanted to test this (assuming you have Edge), just change the url from `https://www.youtube.com/watch?v=dQw4w9WgXcQ` to `microsoft-edge://?url=https://www.youtube.com/watch?v=dQw4w9WgXcQ`.

## Command-line Options

```text
Description:
  Override lock screen content

Usage:
  lockexp [options]

Options:
  -t, --title <title> (REQUIRED)              Title for the lockscreen message
  -d, --description <description> (REQUIRED)  Description for the lockscreen message
  -b, --button <button> (REQUIRED)            Button text
  -u, --url <url> (REQUIRED)                  Button action URL
  --version                                   Show version information
  -?, -h, --help                              Show help and usage information
```

This was written in .NET and Python. The Python script will be updated soon to accept command-line arguments