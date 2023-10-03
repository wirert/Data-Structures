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
        private HashSet<Patient> patients = new HashSet<Patient>();

        public void AddDoctor(Doctor doctor)
            => doctors.Add(doctor.Name, doctor);

        public void AddPatient(Doctor doctor, Patient patient)
        {
            if (!Exist(doctor))
                throw new ArgumentException();

            if(doctor.Patients.Add(patient) == false)
            {
                throw new ArgumentException();
            }

            patients.Add(patient);
            patient.Doctor = doctor;
        }

        public Doctor RemoveDoctor(string name)
        {
            if (!doctors.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            var doctor = doctors[name];

            foreach (var patient in doctor.Patients)
            {
                patients.Remove(patient);
            }

            doctors.Remove(name);

            return doctor;
        }

        public void ChangeDoctor(Doctor oldDoctor, Doctor newDoctor, Patient patient)
        {
            if (!Exist(oldDoctor) || !Exist(newDoctor) || !Exist(patient))
            {
                throw new ArgumentException();
            }

            oldDoctor.Patients.Remove(patient);
            newDoctor.Patients.Add(patient);
            patient.Doctor = newDoctor;
        }

        public bool Exist(Doctor doctor) => doctors.ContainsKey(doctor.Name);

        public bool Exist(Patient patient) => patients.Contains(patient);

        public IEnumerable<Doctor> GetDoctors() => doctors.Values;

        public IEnumerable<Patient> GetPatients() => patients;

        public IEnumerable<Doctor> GetDoctorsByPopularity(int populariry)
            => doctors.Values.Where(d => d.Popularity == populariry);

        public IEnumerable<Doctor> GetDoctorsSortedByPatientsCountDescAndNameAsc()
            => doctors.Values.OrderByDescending(d => d.Patients.Count)
                            .ThenBy(d => d.Name);

        public IEnumerable<Patient> GetPatientsByTown(string town)
            => patients.Where(p => p.Town == town);

        public IEnumerable<Patient> GetPatientsInAgeRange(int lo, int hi)
            => patients.Where(p => p.Age >= lo && p.Age <= hi);

        public IEnumerable<Patient> GetPatientsSortedByDoctorsPopularityAscThenByHeightDescThenByAge()
            => patients.OrderBy(p => p.Doctor.Popularity)
                        .ThenByDescending(p => p.Height)
                        .ThenBy(p => p.Age);

    }
}
