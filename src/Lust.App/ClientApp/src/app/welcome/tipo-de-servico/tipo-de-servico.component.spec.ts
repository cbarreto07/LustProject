import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TipoDeServicoComponent } from './tipo-de-servico.component';

describe('TipoDeServicoComponent', () => {
  let component: TipoDeServicoComponent;
  let fixture: ComponentFixture<TipoDeServicoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TipoDeServicoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TipoDeServicoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
