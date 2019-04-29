import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerfilFotosComponent } from './perfil-fotos.component';

describe('PerfilFotosComponent', () => {
  let component: PerfilFotosComponent;
  let fixture: ComponentFixture<PerfilFotosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerfilFotosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerfilFotosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
