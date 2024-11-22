namespace VaccOps
{
    using Models;
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class VaccDb : IVaccOps
    {
        private Dictionary<string, Doctor> doctors = new Dictionary<string, Doctor>();
        private Dictionary<string, Patient> patients = new Dictionary<string, Patient>();

        public void AddDoctor(Doctor doctor)
        {
            if (doctors.ContainsKey(doctor.Name))
            {
                throw new ArgumentException();
            }

            doctors.Add(doctor.Name, doctor);
        }

        public void AddPatient(Doctor doctor, Patient patient)
        {
            if (!doctors.ContainsKey(doctor.Name))
            {
                throw new ArgumentException();
            }

            patients.Add(patient.Name, patient);
            doctors[doctor.Name].Patients.Add(patient);
            patient.Doctor = doctor;
        }

        public void ChangeDoctor(Doctor oldDoctor, Doctor newDoctor, Patient patient)
        {
            if (!doctors.ContainsKey(oldDoctor.Name)
                || !doctors.ContainsKey(newDoctor.Name)
                || !patients.ContainsKey(patient.Name))
            {
                throw new ArgumentException();
            }

            oldDoctor.Patients.Remove(patient);
            newDoctor.Patients.Add(patient);
            patient.Doctor = newDoctor;
        }

        public bool Exist(Doctor doctor)
            => doctors.ContainsKey(doctor.Name);

        public bool Exist(Patient patient)
            => patients.ContainsKey(patient.Name);

        public IEnumerable<Doctor> GetDoctors()
            => doctors.Values;

        public IEnumerable<Doctor> GetDoctorsByPopularity(int populariry)
            => doctors.Values.Where(d => d.Popularity == populariry);

        public IEnumerable<Doctor> GetDoctorsSortedByPatientsCountDescAndNameAsc()
            => doctors.Values
                .OrderByDescending(d => d.Patients.Count())
                .ThenBy(d => d.Name);
                    

        public IEnumerable<Patient> GetPatients()
            => patients.Values;

        public IEnumerable<Patient> GetPatientsByTown(string town)
            => patients.Values.Where(p => p.Town == town);

        public IEnumerable<Patient> GetPatientsInAgeRange(int lo, int hi)
            => patients.Values.Where(p => p.Age >= lo && p.Age <= hi);

        public IEnumerable<Patient> GetPatientsSortedByDoctorsPopularityAscThenByHeightDescThenByAge()
            => patients.Values
                .OrderBy(p => p.Doctor.Popularity)
                .ThenByDescending(p => p.Height)
                .ThenBy(p => p.Age);
                

        public Doctor RemoveDoctor(string name)
        {
            if(!doctors.ContainsKey(name))
            {
                throw new ArgumentException();
            }
            var doctor = doctors[name];
            doctors.Remove(name);
            foreach (var patient in doctor.Patients)
            {
                patients.Remove(patient.Name);
            }

            return doctor;
        }
    }
}
