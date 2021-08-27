<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128635007/13.1.4%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E995)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
# How to get XtraScheduler synchronized with XtraGrid (XPO version)


<p>This example shows how you can synchronize the XtraGrid with the XtraScheduler. To accomplish this, handle SelectionChanged events. Within the event handler, you should unsubscribe these events, select the appointment representation in another control, update selection information and then subscribe to the events again. To select the appointment in another control, we have to find the selected appointment in the hash table. So, hash tables are created within appropriate event handlers. These tables contain associations between XP objects and appointments or row handles.</p>

<br/>


