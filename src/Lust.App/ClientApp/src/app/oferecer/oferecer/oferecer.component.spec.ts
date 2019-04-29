import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OferecerComponent } from './oferecer.component';

describe('OferecerComponent', () => {
  let component: OferecerComponent;
  let fixture: ComponentFixture<OferecerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OferecerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OferecerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
