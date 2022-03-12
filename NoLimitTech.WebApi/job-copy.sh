#!/bin/bash
cp -rp "/home/qaappsvm/azagent/_work/r1/a/_QA Backend Build Pipeline/drop/NoLimitTech.WebApi" /home/qaappsvm/
sudo systemctl enable kestrel-qa-vsummits-api.service
sudo systemctl restart kestrel-qa-vsummits-api.service
exit 0