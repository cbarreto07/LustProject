import { Component, OnInit, ViewChild, AfterViewInit, HostListener } from '@angular/core';
import { FuseConfigService } from '@fuse/services/config.service';
import { fuseAnimations } from '@fuse/animations';
import { DataService } from 'app/core/services/data.service';
import { ClienteBusca } from '../classes/cliente-busca';
import { FusePerfectScrollbarDirective } from '@fuse/directives/fuse-perfect-scrollbar/fuse-perfect-scrollbar.directive';
import { Platform } from '@angular/cdk/platform';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';


@Component({
  selector: 'appc-desfrutar',
  templateUrl: './desfrutar.component.html',
    styleUrls: ['./desfrutar.component.scss'],
    animations: fuseAnimations
})
export class DesfrutarComponent implements OnInit, AfterViewInit {

   
  
    resultados: ClienteBusca[] = [];
    hasMore = true;
     skip = 0;
     take = 12;
     carregando = false;
   isMobile = false;
  query = "";
  //busca$: Observable<string>;
    
    @ViewChild(FusePerfectScrollbarDirective) directiveScroll: FusePerfectScrollbarDirective;

    constructor(
        private fuseConfig: FuseConfigService,
        private dataService: DataService,
      private platform: Platform,
      private route: ActivatedRoute,
      private router: Router
    ) {

        this.fuseConfig.setConfig({
            layout: {
                navigation: 'left',
                toolbar: 'below'
            }
        });
    }

  ngOnInit() {

    this.route.paramMap.subscribe((params: ParamMap) => {
      this.query = params.get('query') || '';
      if (!this.carregando)
        this.IniciarBusca();
    });

        this.Load();

        if (this.platform.ANDROID || this.platform.IOS) {
            this.isMobile = true;
        }

        if (!this.isMobile && this.directiveScroll) {
            this.directiveScroll.element.nativeElement.addEventListener('ps-y-reach-end', this.Load.bind(this));
        } else {

            this.directiveScroll.element.nativeElement.addEventListener('scroll', this.reachEnd.bind(this));
        }
        
  }

  search(event) {
    const value = event.target.value;
    this.router.navigate(["quero-desfrutar", { query: value }]);
    
  }

  IniciarBusca() {

    this.skip = 0;
    this.take = 12;
    this.hasMore = true;
    this.resultados = [];
    this.Load();
  }



    ngAfterViewInit(): void {
        
    }

    reachEnd() {

        const ch = this.directiveScroll.element.nativeElement.clientHeight;
        const sh = this.directiveScroll.element.nativeElement.scrollHeight;
        const st = this.directiveScroll.element.nativeElement.scrollTop;
        if (!this.carregando && ch + st + 100 > sh)
            this.Load();
    }


    Load() {
        if (this.hasMore && !this.carregando) {
          this.carregando = true;
          let param : { [key: string]: any } = { 'skip' : this.skip, 'take': this.take };
          
          if (this.query)
            param.query = this.query;

          this.dataService.get<ClienteBusca[]>('api/perfis', param)
              .subscribe(
                  result => {
                      this.hasMore = result.length == this.take;
                      this.skip += this.take;
                      this.resultados.push(...result);
                      this.carregando = false;
                  },
                  fail => {
                      this.hasMore = false;
                      this.carregando = false;
                  }
              );
        }
    }

    temResultado() {
        return this.resultados.length > 0 || (this.resultados.length == 0 && !this.carregando)
    }


  corrigeNota(nota) {
    return nota * 5;
  }
}
