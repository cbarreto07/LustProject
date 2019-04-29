import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MatButtonModule, MatIconModule, MatMenuModule, MatProgressBarModule, MatToolbarModule } from '@angular/material';

import { FuseSharedModule } from '@fuse/shared.module';

import { FuseToolbarComponent } from 'app/main/toolbar/toolbar.component';
import { FuseSearchBarModule, FuseShortcutsModule } from '@fuse/components';
import { SharedModule } from 'app/shared/shared.module';

@NgModule({
    declarations: [
        FuseToolbarComponent
    ],
  imports: [
    SharedModule,
        RouterModule,

        MatButtonModule,
        MatIconModule,
        MatMenuModule,
        MatProgressBarModule,
        MatToolbarModule,

        FuseSharedModule,
        FuseSearchBarModule,
      FuseShortcutsModule
      
    ],
    exports     : [
        FuseToolbarComponent
    ]
})
export class FuseToolbarModule
{
}
