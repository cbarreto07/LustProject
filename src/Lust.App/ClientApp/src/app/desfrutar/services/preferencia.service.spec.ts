import { TestBed, inject } from '@angular/core/testing';

import { PreferenciaService } from './preferencia.service';

describe('PreferenciaService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [PreferenciaService]
    });
  });

  it('should be created', inject([PreferenciaService], (service: PreferenciaService) => {
    expect(service).toBeTruthy();
  }));
});
