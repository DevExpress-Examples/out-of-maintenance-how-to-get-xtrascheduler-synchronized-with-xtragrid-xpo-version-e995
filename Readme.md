# How to get XtraScheduler synchronized with XtraGrid (XPO version)


<p>This example shows how you can synchronize the XtraGrid with the XtraScheduler. To accomplish this, handle SelectionChanged events. Within the event handler, you should unsubscribe these events, select the appointment representation in another control, update selection information and then subscribe to the events again. To select the appointment in another control, we have to find the selected appointment in the hash table. So, hash tables are created within appropriate event handlers. These tables contain associations between XP objects and appointments or row handles.</p>

<br/>


