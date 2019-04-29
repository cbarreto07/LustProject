import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { DesfrutarComponent } from './desfrutar.component';

describe('DesfrutarComponent', () => {
  let component: DesfrutarComponent;
  let fixture: ComponentFixture<DesfrutarComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DesfrutarComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DesfrutarComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
