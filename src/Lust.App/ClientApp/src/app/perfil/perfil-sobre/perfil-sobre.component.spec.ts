import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PerfilSobreComponent } from './perfil-sobre.component';

describe('PerfilSobreComponent', () => {
  let component: PerfilSobreComponent;
  let fixture: ComponentFixture<PerfilSobreComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PerfilSobreComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PerfilSobreComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
