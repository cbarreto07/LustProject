import { Component, OnInit } from '@angular/core';
import { FuseConfigService } from '@fuse/services/config.service';


@Component({
  selector: 'appc-oferecer',
  templateUrl: './oferecer.component.html',
  styleUrls: ['./oferecer.component.scss']
})
export class OferecerComponent implements OnInit {

    constructor(
        private fuseConfig: FuseConfigService
    ) {
        this.fuseConfig.setConfig({
            layout: {
                navigation: 'left',
                toolbar: 'below'
            }
        });
    }

  ngOnInit() {
  }

}
